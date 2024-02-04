using AuthMicroservice.BusinessLogic.Dtos;
using AuthMicroservice.BusinessLogic.Enums;
using AuthMicroservice.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthMicroservice.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly IJWTService _jwtService;
        private readonly IAuthenticationService _userAuthenticationService;
        private readonly IRolesService _rolesService;

        public AuthController(IAuthenticationService userAuthenticationService, IJWTService jwtService, IRolesService rolesService)
        {
            _userAuthenticationService = userAuthenticationService;
            _jwtService = jwtService;
            _rolesService = rolesService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] UserRegistrationDto userRegistrationDto)
        {
            var user = await _userAuthenticationService.RegisterUserAsync(userRegistrationDto);
            await _rolesService.AssignRoleToUserAsync(user.Id, Roles.User);

            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] UserLoginDto user)
        {
            var tokenModel = await _userAuthenticationService.LoginUserAsync(user);

            return Ok(tokenModel);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshTokenAsync([FromBody] string refreshToken)
        {
            var newAccessToken = await _jwtService.RenewAccessTokenAsync(refreshToken);

            return Ok(newAccessToken);
        }
    }
}