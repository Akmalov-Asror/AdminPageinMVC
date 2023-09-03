using AdminPageinMVC.Dto;
using AdminPageinMVC.Entity;
using Task = System.Threading.Tasks.Task;

namespace AdminPageinMVC.Repository
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task AddUserAsync(User user);
        Task UpdateUserAsync(int id, UserDTO userDto);
        Task DeleteUserAsync(int id);
        Task<List<CourseDTO>> GetUserCourses(int id);
        Task AddCourseToUser(int courseId);
        Task<User?> GetUserByEmail(string email);
        string GetMyId();

    }
}
