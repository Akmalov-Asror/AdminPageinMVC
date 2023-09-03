using AdminPageinMVC.Data;
using AdminPageinMVC.Dto;
using AdminPageinMVC.Entity;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace AdminPageinMVC.Repository.Repositories;

public class CourseRepository : ICourseRepository
{
    private readonly AppDbContext _context;

    public CourseRepository(AppDbContext context) => _context = context;

    public async Task<List<Course>> GetAllCourseAsync() => await _context.Course.ToListAsync();

    public async Task<Course> GetCourseByIdAsync(int id) => await _context.Course.FirstOrDefaultAsync(c => c.Id == id);

    public async Task AddCourseAsync(Course course)
    {
        _context.Course.Add(course);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateCourseAsync(int id, CourseDTO course)
    {
        var firstOrDefaultAsync = await _context.Course.FirstOrDefaultAsync(u => u.Id == id);

        if (firstOrDefaultAsync != null)
        {
            firstOrDefaultAsync.Description = course.Description;
            firstOrDefaultAsync.Price = course.Price;
            firstOrDefaultAsync.ImageUrl = course.ImageUrl;
            _context.Entry(firstOrDefaultAsync).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteCourseAsync(int id)
    {
        var course = await _context.Course.FindAsync(id);
        if (course != null)
        {
            _context.Course.Remove(course);
            await _context.SaveChangesAsync();
        }
    }
}