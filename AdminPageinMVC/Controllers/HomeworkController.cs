﻿using AdminPageinMVC.Data;
using AdminPageinMVC.Dto;
using AdminPageinMVC.Repository;
using AdminPageinMVC.Repository.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdminPageinMVC.Controllers;

public class HomeworkController : Controller
{
    private readonly AppDbContext _context;
    private readonly IHomeworkRepository _homeworkRepository;

    public HomeworkController(IHomeworkRepository homeworkRepository, AppDbContext context)
    {
        _homeworkRepository = homeworkRepository;
        _context = context;
    }

    public async Task<IActionResult> GetAllHomework()
    {
        var homework =  await _homeworkRepository.GetAllHomeworkAsync();
        return View("_HomeworkPage", homework);
    }

    public async Task<IActionResult> AddHomework() => View("_AddHomework");

    [HttpPost]
    public async Task<ActionResult> AddHomework(string img, string description, int taskId)
    {
        if (!ModelState.IsValid) return View("_HomeworkPage");
        var homework = new HomeworkDTO();
        homework.ImageUrl = img;
        homework.Description = description;
        var findTask = await _context.Task.FirstOrDefaultAsync(c => c.Id == taskId);
        if (findTask != null) homework.Task = findTask;
        await _homeworkRepository.AddHomeworkAsync(homework);
        var allListHomework = await _homeworkRepository.GetAllHomeworkAsync();
        return View("_HomeworkPage", allListHomework);
    }
    public async Task<IActionResult> GetByIdHomework(int id)
    {
        var teacherByIdAsync = await _homeworkRepository.GetHomeworkByIdAsync(id); 
        return View("_ByIdHomework", teacherByIdAsync);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateHomework(int id, string img, string description, int taskId)
    {
        if (!ModelState.IsValid) return View("_HomeworkPage");
        var homeworkDto = new HomeworkDTO();
        homeworkDto.ImageUrl = img;
        homeworkDto.Description = description;
        var findTask = await _context.Task.FirstOrDefaultAsync(c => c.Id == taskId);
        if (findTask != null) homeworkDto.Task = findTask;
        await _homeworkRepository.UpdateHomeworkAsync(id, homeworkDto);
        var allListHomework = await _homeworkRepository.GetAllHomeworkAsync();
        return View("_HomeworkPage", allListHomework);
    }
    public async Task<IActionResult> DeleteHomework(int id)
    {
        await _homeworkRepository.DeleteHomeworkAsync(id);
        var allListHomework = await _homeworkRepository.GetAllHomeworkAsync();
        return View("_HomeworkPage", allListHomework);
    }
}