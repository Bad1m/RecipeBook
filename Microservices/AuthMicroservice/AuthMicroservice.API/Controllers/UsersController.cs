using AuthMicroservice.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthMicroservice.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    [Authorize(Roles = "admin")]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("users")]
        public IActionResult GetAllUsers()
        {
            var users = _userService.GetAllUsers();

            return Ok(users);
        }

        [HttpGet("users/{login}")]
        public async Task<IActionResult> GetUserByLoginAsync(string login)
        {
            var user = await _userService.GetUserByLoginAsync(login);

            return Ok(user);
        }

        [HttpDelete("{login}")]
        public async Task<IActionResult> DeleteUserAsync(string login)
        {
            await _userService.DeleteUserAsync(login);

            return Ok();
        }
    }
}