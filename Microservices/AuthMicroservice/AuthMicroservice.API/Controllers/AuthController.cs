using AuthMicroservice.BusinessLogic.Dtos;
using AuthMicroservice.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AuthMicroservice.API.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IJWTService _jwtService;
        private readonly IUserAuthenticationService _userAuthenticationService;

        public AuthController(IUserAuthenticationService userAuthenticationService, IJWTService jwtService)
        {
            _userAuthenticationService = userAuthenticationService;
            _jwtService = jwtService;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegistrationDto userRegistration)
        {
            var userResult = await _userAuthenticationService.RegisterUserAsync(userRegistration);
            return !userResult.Succeeded ? new BadRequestObjectResult(userResult) : StatusCode(201);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Authenticate([FromBody] UserLoginDto user)
        {
            var authResult = await _userAuthenticationService.IsValidUserAsync(user);
            if (!authResult)
            {
                return Unauthorized();
            }
            var (accessToken, refreshToken) = await _jwtService.CreateTokenAsync();
            return Ok(new { AccessToken = accessToken, RefreshToken = refreshToken });
        }

        [HttpPost("renewaccess")]
        public async Task<IActionResult> RefreshToken([FromBody] string refreshToken)
        {
            var newAccessToken = await _jwtService.RenewAccessTokenAsync(refreshToken);
            return newAccessToken != null ? Ok(new { AccessToken = newAccessToken }) : BadRequest(new { Message = "Invalid refresh token." });
        }

        [HttpGet("user/{login}")]
        public async Task<IActionResult> GetUserByLogin(string login)
        {
            var user = await _userAuthenticationService.GetUserByLoginAsync(login);
            return Ok(user);
        }
    }
}
