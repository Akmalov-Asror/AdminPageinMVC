using AdminPageinMVC.Entity;
using Task = System.Threading.Tasks.Task;

namespace AdminPageinMVC.Repository;

public interface IContactRepository
{
    Task<List<Contact>> GetAll();
    Task AddContact(Contact contact);
    Task Delete(int id);
}