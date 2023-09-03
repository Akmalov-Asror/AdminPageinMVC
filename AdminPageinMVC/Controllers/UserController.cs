using AdminPageinMVC.Dto;
using AdminPageinMVC.Entity;
using AdminPageinMVC.Repository;
using Microsoft.AspNetCore.Mvc;

namespace AdminPageinMVC.Controllers
{
	public class UserController : Controller
	{
		public readonly IUserRepository UserRepository;
		private readonly IFeedbackRepository _feedbackRepository;
		private readonly IResultRepository _resultRepository;

		public UserController(IUserRepository userRepository, IFeedbackRepository feedbackRepository, IResultRepository resultRepository)
		{
			UserRepository = userRepository;
			_feedbackRepository = feedbackRepository;
			_resultRepository = resultRepository;
		}

		public async Task<IActionResult> DeleteUser(int id)
		{
			await UserRepository.DeleteUserAsync(id);
			var allUsersAsync = await UserRepository.GetAllUsersAsync();
			return View("_UserTable");
		}

        [HttpPost]
        public async Task<IActionResult> UpdateUser(int id, string newFullName, string newEmail)
        {
            UserDTO userDto = new UserDTO();
			userDto.FullName = newFullName;
            userDto.Email = newEmail;
            await UserRepository.UpdateUserAsync(id , userDto);
            var allUsersAsync = await UserRepository.GetAllUsersAsync();
			return View("_UserTable", allUsersAsync);
        }

        public async Task<IActionResult> AddUser()
        {
			return View("_AddUser");
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(string fullName, string newEmail, string newPassword)
        {
	        User newUser = new User();
			newUser.FullName = fullName;
			newUser.Email = newEmail;
			newUser.Password = newPassword;
			await UserRepository.AddUserAsync(newUser);

			var allUsersAsync = await UserRepository.GetAllUsersAsync();
			return View("_UserTable", allUsersAsync);
        }

        public async Task<IActionResult> GetUserCourses()
        {

	        return View("_UserCourse");
        }
		[HttpPost]
        public async Task<IActionResult> GetUserCourses(int id)
        {
	        var userCourses = await UserRepository.GetUserCourses(id);

			return View("_UserCourseTable", userCourses);
        }
        public async Task<IActionResult> GetUserFeedbacks()
        {

	        return View("_UserFeedback");
        }
		[HttpPost]
        public async Task<IActionResult> GetUserFeedbacks(int id)
        {
	        var userFeedbacks = await _feedbackRepository.GetUserFeedbacks(id);

	        return View("_UserFeedbackTable", userFeedbacks);
        }
        [HttpPost]
        public async Task<IActionResult> GetUserResults(int id)
        {
	        var userResults = await _resultRepository.GetUserResult(id);

	        return View("_UserResultTable", userResults);
        }
        public async Task<IActionResult> GetUserResults()
        {
	        return View("_UserResult");
        }
        public async Task<IActionResult> GetUserList()
        {
	        var allUsersAsync = await UserRepository.GetAllUsersAsync();

	        return View("_UserTable", allUsersAsync);
        }
	}
}
