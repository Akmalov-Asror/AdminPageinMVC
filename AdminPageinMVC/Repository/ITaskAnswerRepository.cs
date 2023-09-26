using AdminPageinMVC.Entity;

namespace AdminPageinMVC.Repository;

public interface ITaskAnswerRepository
{
    public Task<List<TaskAnswer>> Get();
}
