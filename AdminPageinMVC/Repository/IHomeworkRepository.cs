using AdminPageinMVC.Dto;
using AdminPageinMVC.Entity;
using Task = System.Threading.Tasks.Task;

namespace AdminPageinMVC.Repository;

public interface IHomeworkRepository
{
    Task<List<HomeworkDTO>> GetAllHomeworkAsync();
    Task<HomeworkDTO> GetHomeworkByIdAsync(int id);
    Task AddHomeworkAsync(HomeworkDTO homeworkDto);
    Task UpdateHomeworkAsync(int id, HomeworkDTO homeworkDto);
    Task DeleteHomeworkAsync(int id);
}