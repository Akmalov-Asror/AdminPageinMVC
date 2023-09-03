using AdminPageinMVC.Data;
using AdminPageinMVC.Entity;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace AdminPageinMVC.Repository.Repositories;

public class TestRepository : ITestRepository
{
    private readonly AppDbContext _context;

    public TestRepository(AppDbContext context) => _context = context;

    public async Task<List<Test>> GetAll()
    {
        return await _context.Test.ToListAsync();
    }

    public async Task<Test> GetTestById(int id)
    {
        return await _context.Test.FirstOrDefaultAsync(u => u.Id == id) ?? throw new BadHttpRequestException("Test not found");
    }

    public async Task AddTestAsync(Test test)
    {
        _context.Test.Add(test);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteTestAsync(int id)
    {

        var test = await _context.Test.FindAsync(id);
        if (test != null)
        {
            _context.Test.Remove(test);
            await _context.SaveChangesAsync();
        }
    }

    public async Task UpdateTest(Test test)
    {
        _context.Entry(test).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }
}