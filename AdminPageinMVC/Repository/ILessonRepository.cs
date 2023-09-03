using AdminPageinMVC.Dto;
using AdminPageinMVC.Entity;
using Task = System.Threading.Tasks.Task;

namespace AdminPageinMVC.Repository;

public interface ILessonRepository
{
    Task<List<LessonDTO>> GetAllLessonAsync();
    Task<LessonDTO> GetLessonByIdAsync(int id);
    Task AddLessonAsync(LessonDTO lessonDto);
    Task UpdateLessonAsync(int id, LessonDTO lessonDto);
    Task DeleteLessonAsync(int id);
}