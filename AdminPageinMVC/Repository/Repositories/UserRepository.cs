using System.Security.Claims;
using AdminPageinMVC.Data;
using AdminPageinMVC.Dto;
using AdminPageinMVC.Entity;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace AdminPageinMVC.Repository.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserRepository(AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<User>> GetAllUsersAsync() => await _context.User.ToListAsync();

        public async Task<User> GetUserByIdAsync(int id) => await _context.User.FirstOrDefaultAsync(u => u.Id == id) ?? throw new BadHttpRequestException("User not found");

        public async Task AddUserAsync(User user)
        {
            _context.User.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(int id, UserDTO userDto)
        {

            var firstOrDefaultAsync = await _context.User.FirstOrDefaultAsync(u => u.Id == id);

            if (firstOrDefaultAsync != null)
            {
                firstOrDefaultAsync.FullName = userDto.FullName;
                firstOrDefaultAsync.Email = userDto.Email;
                _context.Entry(firstOrDefaultAsync).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = await _context.User.FindAsync(id);
            if (user != null)
            {
                _context.User.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<CourseDTO>> GetUserCourses(int id)
        {
            var user = await _context.User
                .Include(e => e.Courses)
                .FirstOrDefaultAsync(e => e.Id == id) ?? throw new BadHttpRequestException("User Not found");

            var courseDtos = user.Courses.Select(course => new CourseDTO
            {
                Id = course.Id,
                ImageUrl = course.ImageUrl,
                Description = course.Description,
                Price = course.Price
            }).ToList();

            return courseDtos;
        }

        public async Task AddCourseToUser(int courseId)
        {
            var myId = GetMyId();
            var findAsync = await _context.Course.FindAsync(courseId) ?? throw new BadHttpRequestException("Course not found");

            var user = await _context.User.Include(e => e.Courses).FirstOrDefaultAsync(e => e.Id == Convert.ToInt32(myId)) ?? throw new BadHttpRequestException("User not found");

            user.Courses.Add(findAsync);

            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<User?> GetUserByEmail(string email) => await _context.User.FirstOrDefaultAsync(e => e.Email == email) ?? throw new BadHttpRequestException("User not found");

        public string GetMyId()
        {
            var result = string.Empty;
            if (_httpContextAccessor.HttpContext is not null) result = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return result ?? throw new BadHttpRequestException("User Id not found");
        }
    }
}
