namespace AdminPageinMVC.Entity;

public class Contact 
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? PhoneNumber { get; set; }

    public DateTimeOffset DateTime { get; set; }
}