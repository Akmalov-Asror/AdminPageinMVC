using AdminPageinMVC.Data;
using AdminPageinMVC.Entity;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace AdminPageinMVC.Repository.Repositories;

public class ContactRepository : IContactRepository
{
    private readonly AppDbContext _context;

    public ContactRepository(AppDbContext context) => _context = context;

    public async Task<List<Contact>> GetAll()
    {
        var contacts = await _context.Contact.ToListAsync();

        return contacts;
    }

    public async Task AddContact(Contact contact)
    {
        await _context.Contact.AddAsync(contact);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var contact = await _context.Contact.FindAsync(id);
        if (contact != null)
        {
            _context.Contact.Remove(contact);
            await _context.SaveChangesAsync();
        }
    }
}