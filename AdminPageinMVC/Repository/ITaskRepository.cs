using AdminPageinMVC.Dto;


namespace AdminPageinMVC.Repository;

public interface ITaskRepository
{
    Task<List<TaskDTO>> GetAllTaskAsync();
    Task<TaskDTO> GetTaskByIdAsync(int id);
    Task AddTaskAsync(TaskDTO taskDto);
    Task UpdateTaskAsync(int id, TaskDTO taskDto);
    Task DeleteTaskAsync(int id);
}