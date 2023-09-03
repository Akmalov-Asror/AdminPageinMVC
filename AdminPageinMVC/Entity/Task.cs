using AdminPageinMVC.Entity.ENUMS;

namespace AdminPageinMVC.Entity;

public class Task
{
    public int Id { get; set; }

    public Lesson Lesson { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public DateTimeOffset DateTime { get; set; }

    public EProcess Process { get; set; }
}