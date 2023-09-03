﻿using AdminPageinMVC.Data;
using AdminPageinMVC.Dto;
using AdminPageinMVC.Entity;
using AdminPageinMVC.Repository;
using AdminPageinMVC.Repository.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdminPageinMVC.Controllers;

public class LessonController : Controller
{
      private readonly ILessonRepository _lessonRepository;
      private readonly AppDbContext _context;
      public LessonController(ILessonRepository lessonRepository, AppDbContext context)
      {
          _lessonRepository = lessonRepository;
          _context = context;
      }

      public async Task<ActionResult> GetAllLesson()
      {
          var lesson = await _lessonRepository.GetAllLessonAsync();
          return View("_LessonPage", lesson);
      }
    public async Task<IActionResult> AddLesson() => View("_AddLesson");

      [HttpPost]
      public async Task<IActionResult> AddLesson(int id,string title, string videoUrl, string information, int courseId)
      {
          if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(videoUrl) || string.IsNullOrWhiteSpace(information))
          {
              ModelState.AddModelError("", "Barcha maydonlar to'ldirilishi shart.");
              return View("_LessonPage");
          }
          var lesson = new LessonDTO();
          lesson.Title = title;
          lesson.VideoUrl = videoUrl;
          lesson.Information = information;
            var findCourse = await _context.Course.FirstOrDefaultAsync(c => c.Id == courseId);
        if (findCourse != null) lesson.Course = findCourse;
        await _lessonRepository.AddLessonAsync(lesson);
        var getLessonList = await _lessonRepository.GetAllLessonAsync();
        return View("_LessonPage", getLessonList);
      }



    public async Task<IActionResult> GetByIdEducation(int id)
      {
          var teacherByIdAsync = await _lessonRepository.GetLessonByIdAsync(id);
          return View("_ByIdLesson", teacherByIdAsync);
      }
    [HttpPost]
    public async Task<IActionResult> UpdateLesson(int id, string title, string videoUrl, string information, int courseId)
      {
          if (!ModelState.IsValid) return View("_LessonPage");
          var lesson = new LessonDTO();
          lesson.Title = title;
          lesson.VideoUrl = videoUrl;
          lesson.Information = information;
          var findCourse = await _context.Course.FirstOrDefaultAsync(c => c.Id == courseId);
          if (findCourse != null)
          {
              lesson.Course = findCourse;
          }
          await _lessonRepository.UpdateLessonAsync(id, lesson);
          var allListLesson = await _lessonRepository.GetAllLessonAsync();
          return View("_LessonPage", allListLesson);
    }

    public async Task<IActionResult> DeleteLesson(int id)
    {
        await _lessonRepository.DeleteLessonAsync(id);
        var allListTeachers = await _lessonRepository.GetAllLessonAsync();
        return View("_LessonPage", allListTeachers);
    }

}