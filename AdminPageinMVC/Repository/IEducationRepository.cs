using AdminPageinMVC.Dto;
using AdminPageinMVC.Entity;
using Task = System.Threading.Tasks.Task;

namespace AdminPageinMVC.Repository;

public interface IEducationRepository
{
    Task<List<EducationDTO>> GetAllEducationAsync();
    Task<Education> GetEducationByIdAsync(int id);
    Task AddEducationAsync(EducationDTO educationDto);
    Task UpdateEducationAsync(int id, EducationDTO educationDto);
    Task DeleteEducationAsync(int id);
}