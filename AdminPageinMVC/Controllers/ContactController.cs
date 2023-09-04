using AdminPageinMVC.Entity;
using AdminPageinMVC.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdminPageinMVC.Controllers;

public class ContactController : Controller
{
    private readonly IContactRepository _contactRepository;

    public ContactController(IContactRepository contactRepository) => _contactRepository = contactRepository;

    public async Task<IActionResult> GetContacts()
    {
        var allContacts = await _contactRepository.GetAll();
        return View("_ContactPage", allContacts);
    }
    public async Task<IActionResult> DeleteContact(int id)
    {
        await _contactRepository.Delete(id);
        var contacts = await _contactRepository.GetAll();
        return View("_ContactPage", contacts);
    }
    public async Task<IActionResult> AddContact() => View("_AddContact");
    
    [HttpPost]
    public async Task<IActionResult> AddContact(string name, string phoneNumber)
    {
        if (!ModelState.IsValid) return View("_ContactPage");
        var addContact = new Contact()
        {
            Name = name,
            PhoneNumber = phoneNumber,
            DateTime = DateTimeOffset.UtcNow
        };
        await _contactRepository.AddContact(addContact);
        var allContacts = await _contactRepository.GetAll();
        return View("_ContactPage", allContacts);
    }
}