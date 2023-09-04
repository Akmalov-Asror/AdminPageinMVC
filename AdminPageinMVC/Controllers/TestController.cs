﻿using AdminPageinMVC.Dto;
using AdminPageinMVC.Entity;
using AdminPageinMVC.Repository;
using AdminPageinMVC.Repository.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdminPageinMVC.Controllers;

public class TestController : Controller
{
    private readonly ITestRepository _testRepository;

    public TestController(ITestRepository testRepository) => _testRepository = testRepository;

    public async Task<IActionResult> GetAllTest()
    {
        var allTests = await _testRepository.GetAll();
        return View("_TestPage", allTests);
    }

    public async Task<IActionResult> AddTest() => View("_AddTest");

    [HttpPost]
    public async Task<IActionResult> AddTest(string question, List<string> options, string right)
    {
        if (!ModelState.IsValid) return View("_TestPage");
        Test test = new Test();
        test.Question = question;
        test.Options = options;
        test.RightOption = right;
        await _testRepository.AddTestAsync(test);
        var allTests = await _testRepository.GetAll();
        return View("_TestPage", allTests);
    }
    public async Task<IActionResult> GetByIdTask(int id)
    {
        var testByIdAsync = await _testRepository.GetTestById(id);
        return View("_ByIdTest", testByIdAsync);
    }
    [HttpPost]
    public async Task<IActionResult> UpdateTest(int id, string question, List<string> options, string right)
    {
        if (!ModelState.IsValid) return View("_TestPage");
        var test = new Test();
        test.Question = question;
        test.Options = options;
        test.RightOption = right;
        await _testRepository.UpdateTest(test);
        var tests = await _testRepository.GetAll();
        return View("_TestPage", tests);
    }
    public async Task<IActionResult> DeleteTest(int id)
    {
        await _testRepository.DeleteTestAsync(id);
        var allListTest = await _testRepository.GetAll();
        return View("_TestPage", allListTest);
    }
}