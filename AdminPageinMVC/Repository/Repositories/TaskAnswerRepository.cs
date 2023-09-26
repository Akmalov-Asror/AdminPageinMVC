using AdminPageinMVC.Data;
using AdminPageinMVC.Entity;
using Microsoft.EntityFrameworkCore;

namespace AdminPageinMVC.Repository.Repositories;

public class TaskAnswerRepository : ITaskAnswerRepository
{
    private readonly AppDbContext _context;

    public TaskAnswerRepository(AppDbContext context) => _context = context;
    public async Task<List<TaskAnswer>> Get() => await _context.TaskAnswers.Include(t => t.Task).ToListAsync();
}