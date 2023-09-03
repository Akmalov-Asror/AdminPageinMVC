using AdminPageinMVC.Data;
using AdminPageinMVC.Dto;
using AdminPageinMVC.Repository;
using AdminPageinMVC.Repository.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdminPageinMVC.Controllers;

public class EducationController : Controller
{
    private readonly IEducationRepository _educationRepository;
    private readonly AppDbContext _context;
    public EducationController(IEducationRepository educationRepository, AppDbContext context)
    {
        _educationRepository = educationRepository;
        _context = context;
    }

    public async Task<ActionResult> GetAllEducation()
    {
        var education = await _educationRepository.GetAllEducationAsync();
        return View("_EducationPage", education);
    }

    public async Task<IActionResult> AddEducation() => View("_AddEducation");   

    [HttpPost]
    public async Task<ActionResult> AddEducation(string title, string end, string description, int courseId)
    {
        if (!ModelState.IsValid) return View("_EducationPage");
        var edu = new EducationDTO();
        edu.Title = title;
        edu.End = end;
        edu.Description = description;
        var findCourse = await _context.Course.FirstOrDefaultAsync(c => c.Id == courseId);
        if (findCourse != null)
        {
            edu.Course = findCourse;
        }

        await _educationRepository.AddEducationAsync(edu);
        var getEducationList = await _educationRepository.GetAllEducationAsync();
        return View("_EducationPage", getEducationList);
    }
    public async Task<IActionResult> GetByIdEducation(int id)
    {
        var teacherByIdAsync = await _educationRepository.GetEducationByIdAsync(id);
        return View("_ByIdEducation", teacherByIdAsync);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateEducation(int id, string title, string description, string end, int courseId)
    {
        if (!ModelState.IsValid) return View("_EducationPage");
        var education = new EducationDTO();
        education.Title = title;
        education.Description = description;
        education.End = end;
        var findCourse = await _context.Course.FirstOrDefaultAsync(c => c.Id == courseId);
        if (findCourse != null)
        {
            education.Course = findCourse;
        }
        await _educationRepository.UpdateEducationAsync(id, education);
        var allListTeachers = await _educationRepository.GetAllEducationAsync();
        return View("_EducationPage", allListTeachers);
    }
    public async Task<IActionResult> DeleteCourse(int id)
    {
        await _educationRepository.DeleteEducationAsync(id);
        var allListTeachers = await _educationRepository.GetAllEducationAsync();
        return View("_EducationPage", allListTeachers);
    }

}