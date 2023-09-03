using AdminPageinMVC.Entity;
using AdminPageinMVC.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdminPageinMVC.Controllers;

public class ResultController : Controller
{
    private readonly IResultRepository _resultRepository;
    private readonly IEducationRepository _educationRepository;
    private readonly IUserRepository _userRepository;

    public ResultController(IResultRepository resultRepository, IEducationRepository educationRepository, IUserRepository userRepository)
    {
        _resultRepository = resultRepository;
        _educationRepository = educationRepository;
        _userRepository = userRepository;
    }

    public async Task<IActionResult> GetResults()
    {
        var allResult = await _resultRepository.GetAllResultAsync();

        return View("ResultTable", allResult);
    }

    public async Task<IActionResult> AddResult()
    {
        ViewBag.EducationList = await _educationRepository.GetAllEducationAsync();
        ViewBag.UserList = await _userRepository.GetAllUsersAsync();

        return View("AddResult");
    }
    [HttpPost]
    public async Task<IActionResult> AddResult(string url, int educationId, int userId)
    {
        Result result = new Result();
        result.Url = url;
        result.User = await _userRepository.GetUserByIdAsync(userId);
        result.Education = await _educationRepository.GetEducationByIdAsync(educationId);
        await _resultRepository.AddResultAsync(result);
        var allResultAsync = await _resultRepository.GetAllResultAsync();
        return View("ResultTable", allResultAsync);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateResult(int id, string url, int educationId, int userId)
    {
        var result = await _resultRepository.GetResultByIdAsync(id);
        result.Url = url;
        result.Education = await _educationRepository.GetEducationByIdAsync(educationId);
        result.User = await _userRepository.GetUserByIdAsync(userId);
        await _resultRepository.UpdateResultAsync(result);
        var all = await _resultRepository.GetAllResultAsync();
        return View("ResultTable", all);
    }
    public async Task<IActionResult> UpdateResult()
    {
        return View("GetById");
    }

    [HttpPost]
    public async Task<IActionResult> GetResultById(int id)
    {
        var resultByIdAsync = await _resultRepository.GetResultByIdAsync(id);
        ViewBag.EducationList = await _educationRepository.GetAllEducationAsync();
        ViewBag.UserList = await _userRepository.GetAllUsersAsync();
        return View("UpdateResult", resultByIdAsync);
    }

    public async Task<IActionResult> DeleteResult()
    {
        return View("DeleteResult");
    }
    [HttpPost]
    public async Task<IActionResult> DeleteResult(int id)
    {
        await _resultRepository.DeleteResultAsync(id);
        var all = await _resultRepository.GetAllResultAsync();
        return View("ResultTable", all);
    }
}