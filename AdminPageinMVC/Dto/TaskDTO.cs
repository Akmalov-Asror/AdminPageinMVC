using AdminPageinMVC.Entity.ENUMS;
using AdminPageinMVC.Entity;

namespace AdminPageinMVC.Dto;

public class TaskDTO
{
    public int Id { get; set; }
    public Lesson Lesson { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTimeOffset DateTime { get; set; }
    public EProcess Process { get; set; }
}