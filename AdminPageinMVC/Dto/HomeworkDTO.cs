using Task = AdminPageinMVC.Entity.Task;

namespace AdminPageinMVC.Dto;

public class HomeworkDTO
{
    public int Id { get; set; }

    public string? ImageUrl { get; set; }

    public string? Description { get; set; }

    public Task Task { get; set; }
}