using AdminPageinMVC.Dto;
using AdminPageinMVC.Entity;
using Task = System.Threading.Tasks.Task;

namespace AdminPageinMVC.Repository;

public interface ITeacherRepository
{
    Task<List<Teacher>> GetAllTeacherAsync();
    Task<Teacher> GetTeacherByIdAsync(int id);
    Task AddTeacherAsync(Teacher teacher);
    Task UpdateTeacherAsync(int id, TeacherDto teacher);
    Task DeleteTeacherAsync(int id);

}