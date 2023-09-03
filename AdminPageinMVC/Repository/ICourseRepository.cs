using AdminPageinMVC.Dto;
using AdminPageinMVC.Entity;
using Task = System.Threading.Tasks.Task;

namespace AdminPageinMVC.Repository;

public interface ICourseRepository
{
    Task<List<Course>> GetAllCourseAsync();
    Task<Course> GetCourseByIdAsync(int id);
    Task AddCourseAsync(Course course);
    Task UpdateCourseAsync(int id,CourseDTO teacher);
    Task DeleteCourseAsync(int id);
}