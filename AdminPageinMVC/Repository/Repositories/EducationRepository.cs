using AdminPageinMVC.Data;
using AdminPageinMVC.Dto;
using AdminPageinMVC.Entity;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace AdminPageinMVC.Repository.Repositories;

public class EducationRepository : IEducationRepository
{
    private readonly AppDbContext _context;
    public EducationRepository(AppDbContext context) => _context = context;

    public async Task<List<EducationDTO>> GetAllEducationAsync()
    {
        var education = await _context.Education
            .Include(e => e.Course)
            .Select(e => new EducationDTO()
            {
                Id = e.Id,
                Description = e.Description,
                Course = e.Course,
                End = e.End,
                Title = e.Title
            })
            .ToListAsync();
        return education;
    }

    public async Task<Education> GetEducationByIdAsync(int id)
    {
        var education = await _context.Education
            .Include(e => e.Course)
            .FirstOrDefaultAsync(e => e.Id == id) ?? throw new BadHttpRequestException("Not Found");
        var educationDto = new EducationDTO();
        educationDto.Id = id;
        educationDto.Description = education.Description;
        educationDto.Title = education.Title;
        educationDto.End = education.End;
        educationDto.Course = education.Course;
        return education;
    }

    public async Task AddEducationAsync(EducationDTO educationDto)
    {
        var education = new Education();
        education.Description = educationDto.Description;
        education.End = educationDto.End;
        education.Title = educationDto.Title;
        var findCourse = await _context.Course.FindAsync(educationDto.Course.Id);
        if (findCourse != null) education.Course = educationDto.Course;
        _context.Education.Add(education);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateEducationAsync(int id, EducationDTO educationDto)
    {
        var educationAsync = await _context.Education.FirstOrDefaultAsync(e => e.Id == id);
        educationAsync.Id = id;
        educationAsync.Description = educationDto.Description;
        educationAsync.Title = educationDto.Title;
        educationAsync.End = educationDto.End;
        educationAsync.Course = educationDto.Course;

        _context.Entry(educationAsync).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteEducationAsync(int id)
    {
        var education = await _context.Education.FindAsync(id);
        if (education != null)
        {
            _context.Education.Remove(education);
            await _context.SaveChangesAsync();
        }
    }
}