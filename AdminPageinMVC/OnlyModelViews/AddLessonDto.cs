using AdminPageinMVC.Entity;

namespace AdminPageinMVC.OnlyModelViews;

public class AddLessonDto
{
    public string? Title { get; set; }

    public string? VideoUrl { get; set; }

    public string? Information { get; set; }
    public int CourseId { get; set; }
}