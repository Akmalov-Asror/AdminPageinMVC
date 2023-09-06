using AdminPageinMVC.Dto;
using AdminPageinMVC.Entity;
using AdminPageinMVC.OnlyModelViews;
using AdminPageinMVC.Repository;
using AdminPageinMVC.Repository.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AdminPageinMVC.Controllers;

public class TeacherController : Controller
{
	private readonly ITeacherRepository _teacherRepository;

	public TeacherController(ITeacherRepository teacherRepository) => _teacherRepository = teacherRepository;

	public async Task<IActionResult> GetTeacherList()
	{
		var allTeacherAsync = await _teacherRepository.GetAllTeacherAsync();

		return View("_TeacherCard", allTeacherAsync);
	}
	public async Task<IActionResult> AddTeacher() => View("_AddTeacher");

    [HttpPost]
	public async Task<IActionResult> AddTeacher(AddTeacherDto addTeacherDto)
	{
        if (!ModelState.IsValid) return View("_TeacherCard");
        var teacher = new Teacher();
		teacher.Name = addTeacherDto.Name;
		teacher.Type = addTeacherDto.Type;
		teacher.ImageUrl = addTeacherDto.ImageUrl;
		await _teacherRepository.AddTeacherAsync(teacher);
		var allListTeachers = await _teacherRepository.GetAllTeacherAsync();
		return View("_TeacherCard", allListTeachers);
	}

    
    public async Task<IActionResult> UpdateTeacher()
    {
        return View("_UpdateTeacherInfo");
    }

    public async Task<IActionResult> GetById(int id)
    {
        if (!ModelState.IsValid) return View("_TeacherCard");
        var teacherByIdAsync = await _teacherRepository.GetTeacherByIdAsync(id);
        return View("_ById", teacherByIdAsync);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateTeacher(int id, AddTeacherDto addTeacherDto)
    {
        if (!ModelState.IsValid) return View("_TeacherCard");
        var teacher = new TeacherDto();
        teacher.Name = addTeacherDto.Name;
		teacher.Type = addTeacherDto.Type;
		teacher.ImageUrl = addTeacherDto.ImageUrl;
        await _teacherRepository.UpdateTeacherAsync(id, teacher);
        var allListTeachers = await _teacherRepository.GetAllTeacherAsync();
        return View("_TeacherCard", allListTeachers);
    }
    public async Task<IActionResult> DeleteTeacher() => View("_DeleteTeacher");
    [HttpPost]
    public async Task<IActionResult> DeleteTeacher(int id)
    {
        if (!ModelState.IsValid) return View("_Teacher");
        await _teacherRepository.DeleteTeacherAsync(id);
        var allListTeachers = await _teacherRepository.GetAllTeacherAsync();
        return View("_TeacherCard", allListTeachers);
    }

}