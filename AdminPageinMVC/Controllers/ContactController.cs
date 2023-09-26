using AdminPageinMVC.Entity;
using AdminPageinMVC.OnlyModelViews;
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
        return View(allContacts);
    }
    public async Task<IActionResult> DeleteContact(int id)
    {
        await _contactRepository.Delete(id);
        var contacts = await _contactRepository.GetAll();
        return View("GetContacts", contacts);
    }
    public async Task<IActionResult> AddContact() => View("_AddContact");
    
    [HttpPost]
    public async Task<IActionResult> AddContact(ContactDto contactDto)
    {
        if (!ModelState.IsValid) return View("GetContacts");
        var addContact = new Contact()
        {
            Name = contactDto.name,
            PhoneNumber = contactDto.phoneNumber,
            DateTime = "12.09.2023"
        };
        await _contactRepository.AddContact(addContact);
        var allContacts = await _contactRepository.GetAll();
        return View("GetContacts", allContacts);
    }
}