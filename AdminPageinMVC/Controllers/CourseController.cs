using AdminPageinMVC.Dto;
using AdminPageinMVC.Entity;
using AdminPageinMVC.Repository;
using AdminPageinMVC.Repository.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using AdminPageinMVC.OnlyModelViews;
using static System.Net.Mime.MediaTypeNames;

namespace AdminPageinMVC.Controllers;

public class CourseController : Controller
{
    private readonly ICourseRepository _courseRepository;

    public CourseController(ICourseRepository courseRepository) => _courseRepository = courseRepository;

    public async Task<IActionResult> GetCourseList()
    {
        var allCourseAsync = await _courseRepository.GetAllCourseAsync();

        return View("_CourseCard", allCourseAsync);
    }

    public async Task<IActionResult> AddCourse() => View("_AddCourse");

    [HttpPost]
    public async Task<IActionResult> AddCourse(AddCourseDto addCourseDto)
    {
        if (!ModelState.IsValid) return View("_CourseCard");
        var course = new Course
        {
            Price = Convert.ToDouble(addCourseDto.Price),
            Description = addCourseDto.Description,
            ImageUrl = addCourseDto.ImageUrl
        };
        await _courseRepository.AddCourseAsync(course);
        var allListCourses = await _courseRepository.GetAllCourseAsync();
        return View("_CourseCard", allListCourses);
    }

    public async Task<IActionResult> UpdateCourse() => View("_UpdateCourseInfo");

    public async Task<IActionResult> GetByIdCourse(int id)
    {
        var teacherByIdAsync = await _courseRepository.GetCourseByIdAsync(id);
        return View("_ByIdCourse", teacherByIdAsync);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateCourse(int id , AddCourseDto addCourseDto)
    {
        if (!ModelState.IsValid) return View("_CourseCard");
        var teacher = new CourseDTO();
        teacher.Price = addCourseDto.Price;
        teacher.Description = addCourseDto.Description;
        teacher.ImageUrl = addCourseDto.ImageUrl;
        await _courseRepository.UpdateCourseAsync(id, teacher);
        var allListTeachers = await _courseRepository.GetAllCourseAsync();
        return View("_CourseCard", allListTeachers);
    }
    public async Task<IActionResult> DeleteCourse() => View("_DeleteCourse");
    [HttpPost]
    public async Task<IActionResult> DeleteCourse(int id)
    {
        await _courseRepository.DeleteCourseAsync(id);
        var allListTeachers = await _courseRepository.GetAllCourseAsync();
        return View("_CourseCard", allListTeachers);
    }
}