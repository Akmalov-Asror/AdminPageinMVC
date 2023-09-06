namespace AdminPageinMVC.OnlyModelViews;

public class AddTestDto
{
    public string? Question { get; set; }
    public List<string>? Options { get; set; }
    public string? RightOption { get; set; }
}