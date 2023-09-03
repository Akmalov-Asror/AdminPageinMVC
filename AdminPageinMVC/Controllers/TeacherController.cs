using AdminPageinMVC.Dto;
using AdminPageinMVC.Entity;
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
	public async Task<IActionResult> AddTeacher(string name, string type , string imgUrl)
	{
		var teacher = new Teacher();
		teacher.Name = name;
		teacher.Type = type;
		teacher.ImageUrl = imgUrl;
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
        var teacherByIdAsync = await _teacherRepository.GetTeacherByIdAsync(id);
        return View("_ById", teacherByIdAsync);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateTeacher(int id,string name, string type, string image)
    {

		var teacher = new TeacherDto();
        teacher.Name = name;
		teacher.Type = type;
		teacher.ImageUrl = image;
        await _teacherRepository.UpdateTeacherAsync(id, teacher);
        var allListTeachers = await _teacherRepository.GetAllTeacherAsync();
        return View("_TeacherCard", allListTeachers);
    }
    public async Task<IActionResult> DeleteTeacher() => View("_DeleteTeacher");
    [HttpPost]
    public async Task<IActionResult> DeleteTeacher(int id)
    {
        await _teacherRepository.DeleteTeacherAsync(id);
        var allListTeachers = await _teacherRepository.GetAllTeacherAsync();
        return View("_TeacherCard", allListTeachers);
    }

}