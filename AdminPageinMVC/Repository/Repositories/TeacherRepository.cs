using AdminPageinMVC.Data;
using AdminPageinMVC.Entity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using AdminPageinMVC.Dto;
using Task = System.Threading.Tasks.Task;

namespace AdminPageinMVC.Repository.Repositories;

public class TeacherRepository : ITeacherRepository
{
	private readonly AppDbContext _context;

	public TeacherRepository(AppDbContext context) => _context = context;

	public Task<List<Teacher>> GetAllTeacherAsync()
	{
		return _context.Teacher.ToListAsync();
	}

	public async Task<Teacher> GetTeacherByIdAsync(int id)
	{
		return await _context.Teacher.FirstOrDefaultAsync(x => x.Id == id);
	}

	public async Task AddTeacherAsync(Teacher teacher)
	{
		_context.Teacher.Add(teacher);
		await _context.SaveChangesAsync();
	}

    public async Task UpdateTeacherAsync(int id, TeacherDto teacher)
	{
        var firstOrDefaultAsync = await _context.Teacher.FirstOrDefaultAsync(u => u.Id == id);

        if (firstOrDefaultAsync != null)
        {
            firstOrDefaultAsync.Name = teacher.Name;
            firstOrDefaultAsync.Type = teacher.Type;
            firstOrDefaultAsync.ImageUrl = teacher.ImageUrl;
            _context.Entry(firstOrDefaultAsync).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }

	public async Task DeleteTeacherAsync(int id)
    {
        var teacher = await _context.Teacher.FindAsync(id);
        if (teacher != null)
        {
            _context.Teacher.Remove(teacher);
            await _context.SaveChangesAsync();
        }
    }
}