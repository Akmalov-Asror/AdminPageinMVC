using AdminPageinMVC.Dto;
using AdminPageinMVC.Entity;
using AdminPageinMVC.Repository;
using AdminPageinMVC.Repository.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
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
    public async Task<IActionResult> AddCourse(string price, string description, string imgUrl)
    {
        var course = new Course();
        course.Price = Convert.ToDouble(price);
        course.Description = description;
        course.ImageUrl = imgUrl;
        await _courseRepository.AddCourseAsync(course);
        var allListCourses = await _courseRepository.GetAllCourseAsync();
        return View("_CourseCard", allListCourses);
    }

    public async Task<IActionResult> UpdateCourse()
    {
        return View("_UpdateCourseInfo");
    }

    public async Task<IActionResult> GetByIdCourse(int id)
    {
        var teacherByIdAsync = await _courseRepository.GetCourseByIdAsync(id);
        return View("_ByIdCourse", teacherByIdAsync);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateCourse(int id, double price, string description, string imgUrl)
    {
        var teacher = new CourseDTO();
        teacher.Price = price;
        teacher.Description = description;
        teacher.ImageUrl = imgUrl;
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