using AdminPageinMVC.Entity;
using AdminPageinMVC.OnlyModelViews;
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
    public async Task<IActionResult> AddResult(AddResultDto addResultDto)
    {
        if (!ModelState.IsValid) return View("ResultTable");
        Result result = new Result();
        result.Url = addResultDto.Url;
        result.User = await _userRepository.GetUserByIdAsync(addResultDto.UserId);
        result.Education = await _educationRepository.GetEducationByIdAsync(addResultDto.EducationId);
        await _resultRepository.AddResultAsync(result);
        var allResultAsync = await _resultRepository.GetAllResultAsync();
        return View("ResultTable", allResultAsync);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateResult(int id, AddResultDto addResultDto)
    {
        if (!ModelState.IsValid) return View("ResultTable");
        var result = await _resultRepository.GetResultByIdAsync(id);
        result.Url = addResultDto.Url;
        result.Education = await _educationRepository.GetEducationByIdAsync(addResultDto.EducationId);
        result.User = await _userRepository.GetUserByIdAsync(addResultDto.UserId);
        await _resultRepository.UpdateResultAsync(result);
        var all = await _resultRepository.GetAllResultAsync();
        return View("ResultTable", all);
    }
    public async Task<IActionResult> UpdateResult() => View("GetById");

    [HttpPost]
    public async Task<IActionResult> GetResultById(int id)
    {
        if (!ModelState.IsValid) return View("ResultTable");
        var resultByIdAsync = await _resultRepository.GetResultByIdAsync(id);
        ViewBag.EducationList = await _educationRepository.GetAllEducationAsync();
        ViewBag.UserList = await _userRepository.GetAllUsersAsync();
        return View("UpdateResult", resultByIdAsync);
    }

    public async Task<IActionResult> DeleteResult() => View("DeleteResult");

    [HttpPost]
    public async Task<IActionResult> DeleteResult(int id)
    {
        if (!ModelState.IsValid) return View("ResultTable");
        await _resultRepository.DeleteResultAsync(id);
        var all = await _resultRepository.GetAllResultAsync();
        return View("ResultTable", all);
    }
}