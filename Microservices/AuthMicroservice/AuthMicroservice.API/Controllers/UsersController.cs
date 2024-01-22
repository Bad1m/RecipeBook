using AuthMicroservice.API.Attributes;
using AuthMicroservice.BusinessLogic.Enums;
using AuthMicroservice.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AuthMicroservice.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    [AuthorizeRoles(Roles.admin)]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers(int page = 1, int pageSize = 10)
        {
            var users = await _userService.GetAllUsersAsync(page, pageSize);

            return Ok(users);
        }

        [HttpGet("{login}")]
        public async Task<IActionResult> GetUserByLoginAsync(string login)
        {
            var user = await _userService.GetUserDtoByLoginAsync(login);

            return Ok(user);
        }

        [HttpDelete("{login}")]
        public async Task<IActionResult> DeleteUserAsync(string login)
        {
            await _userService.DeleteUserAsync(login);

            return NoContent();
        }
    }
}