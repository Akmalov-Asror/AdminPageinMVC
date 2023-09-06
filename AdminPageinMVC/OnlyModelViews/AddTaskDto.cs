namespace AdminPageinMVC.OnlyModelViews;

public class AddTaskDto
{
    public int LessonId { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
}