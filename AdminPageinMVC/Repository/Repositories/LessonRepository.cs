using AdminPageinMVC.Data;
using AdminPageinMVC.Dto;
using AdminPageinMVC.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace AdminPageinMVC.Repository.Repositories;

public class LessonRepository : ILessonRepository
{
    private readonly AppDbContext _context;

    public LessonRepository(AppDbContext context) => _context = context;

    public async Task<List<LessonDTO>> GetAllLessonAsync()
    {
        var education = await _context.Lesson
            .Include(e => e.Course)
            .Select(e => new LessonDTO()
            {
                Id = e.Id,
                Title = e.Title,
                VideoUrl = e.VideoUrl,
                Information = e.Information,
                Course = e.Course
            })
            .ToListAsync();
        return education;
    }

    public async Task<LessonDTO> GetLessonByIdAsync(int id)
    {
        var education = await _context.Lesson
            .Include(e => e.Course)
            .FirstOrDefaultAsync(e => e.Id == id) ?? throw new BadHttpRequestException("Not Found");
        var lesson = new LessonDTO();
        lesson.Id = id;
        lesson.Title = education.Title;
        lesson.VideoUrl = education.VideoUrl;
        lesson.Information = education.Information;
        lesson.Course = education.Course;
        return lesson;
    }
    public async Task AddLessonAsync(LessonDTO lessonDto)
    {
        var lesson = new Lesson();
        lesson.Title = lessonDto.Title;
        lesson.Information = lessonDto.Information;
        lesson.VideoUrl = lessonDto.VideoUrl;
        var findLesson = await _context.Course.FindAsync(lessonDto.Course.Id);
        if (findLesson != null) lesson.Course = lessonDto.Course;
        _context.Lesson.Add(lesson);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateLessonAsync(int id, LessonDTO lessonDto)
    {
        var lessonFind = await _context.Lesson.FirstOrDefaultAsync(l => l.Id == id);
        lessonFind.Id = id;
        lessonFind.Title = lessonDto.Title;
        lessonFind.Information = lessonDto.Information;
        lessonFind.VideoUrl = lessonDto.VideoUrl;
        lessonFind.Course = lessonDto.Course;

        _context.Entry(lessonFind).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteLessonAsync(int id)
    {
        var lesson = await _context.Lesson.FindAsync(id);
        if (lesson != null)
        {
            _context.Lesson.Remove(lesson);
            await _context.SaveChangesAsync();
        }
    }
}