using System.ComponentModel.DataAnnotations;

namespace AdminPageinMVC.Dto;

public class LoginDTO
{
	public string? Email { get; set; }
	public string? Password { get; set; }
}