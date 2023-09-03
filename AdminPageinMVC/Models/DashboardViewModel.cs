using AdminPageinMVC.Dto;
using AdminPageinMVC.Entity;

namespace AdminPageinMVC.Models;

public class DashboardViewModel
{
	public User CurrentUser { get; set; }
	public List<User> AllUsers { get; set; }
	public List<CourseDTO> Courses { get; set; }
	public string State { get; set; }
}