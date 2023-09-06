﻿using AdminPageinMVC.Data;
using AdminPageinMVC.Dto;
using AdminPageinMVC.Entity;
using AdminPageinMVC.Entity.ENUMS;
using AdminPageinMVC.Repository;
using AdminPageinMVC.Repository.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using AdminPageinMVC.OnlyModelViews;

namespace AdminPageinMVC.Controllers;

public class TaskController : Controller
{
    private readonly ITaskRepository _taskRepository;
    private readonly AppDbContext _context;
    public TaskController(ITaskRepository taskRepository, AppDbContext context)
    {
        _taskRepository = taskRepository;
        _context = context;
    }
    public async Task<IActionResult> GetAllTask()
    {
        var allListTask = await _taskRepository.GetAllTaskAsync();
        return View("_TaskPage", allListTask);
    }

    public async Task<IActionResult> AddTask() => View("_AddTask");

    [HttpPost]
    public async Task<ActionResult> AddTask(AddTaskDto lessonDto)
    {
        if (!ModelState.IsValid) return View("_TaskPage");

        var task = new TaskDTO
        {
            Process = EProcess.PR0GRESS,
            DateTime = DateTimeOffset.UtcNow,
            Title = lessonDto.Title,
            Description = lessonDto.Description,
        };

        var findLesson = await _context.Lesson.FirstOrDefaultAsync(c => c.Id == lessonDto.LessonId);
        if (findLesson != null) task.Lesson = findLesson;

        await _taskRepository.AddTaskAsync(task);

        var allListTask = await _taskRepository.GetAllTaskAsync();
        return View("_TaskPage", allListTask);
    }
    public async Task<IActionResult> GetByIdTask(int id)
    {
        if (!ModelState.IsValid) return View("_TaskPage");
        var taskByIdAsync = await _taskRepository.GetTaskByIdAsync(id);
        return View("_ByIdTask", taskByIdAsync);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateTask(int id, AddTaskDto lessonDto)
    {
        if (!ModelState.IsValid) return View("_TaskPage");
        var task = new TaskDTO();
        task.Process = EProcess.PR0GRESS;
        task.DateTime = DateTimeOffset.UtcNow;
        task.Title = lessonDto.Title;
        task.Description = lessonDto.Description;
        var findLesson = await _context.Lesson.FirstOrDefaultAsync(c => c.Id == lessonDto.LessonId);
        if (findLesson != null) task.Lesson = findLesson;
        await _taskRepository.UpdateTaskAsync(id, task);
        var allListTask = await _taskRepository.GetAllTaskAsync();
        return View("_TaskPage", allListTask);
    }
    public async Task<IActionResult> DeleteTask(int id)
    {
        await _taskRepository.DeleteTaskAsync(id);
        var allListTask = await _taskRepository.GetAllTaskAsync();
        return View("_TaskPage", allListTask);
    }
}