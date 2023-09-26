using AdminPageinMVC.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdminPageinMVC.Controllers;

public class TaskAnswerController : Controller
{
    private readonly ITaskAnswerRepository _taskAnswerRepository;

    public TaskAnswerController(ITaskAnswerRepository taskAnswerRepository) => _taskAnswerRepository = taskAnswerRepository;

    [HttpGet]
    public async Task<IActionResult> GetAllAnswer()
    {
        var taskAnswer = await _taskAnswerRepository.Get();
        Console.Write(taskAnswer);
        return View("_TaskList", taskAnswer);   
    }
}