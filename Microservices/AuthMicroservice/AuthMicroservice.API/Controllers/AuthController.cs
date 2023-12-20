using AuthMicroservice.BusinessLogic.Dtos;
using AuthMicroservice.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AuthMicroservice.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IJWTService _jwtService;
        private readonly IAuthenticationService _userAuthenticationService;

        public AuthController(IAuthenticationService userAuthenticationService, IJWTService jwtService)
        {
            _userAuthenticationService = userAuthenticationService;
            _jwtService = jwtService;
        }

        [HttpPost("register/user")]
        public async Task<IActionResult> RegisterUserAsync([FromBody] UserRegistrationDto userRegistration)
        {
            await _userAuthenticationService.RegisterUserAsync(userRegistration);

            return Ok(userRegistration);
        }

        [HttpPost("register/admin")]
        public async Task<IActionResult> RegisterAdminAsync([FromBody] UserRegistrationDto adminRegistration)
        {
            await _userAuthenticationService.RegisterAdminAsync(adminRegistration);

            return Ok(adminRegistration);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] UserLoginDto user)
        {
            var tokenModel = await _userAuthenticationService.LoginUserAsync(user);

            return Ok(new { tokenModel.AccessToken, tokenModel.RefreshToken });
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshTokenAsync([FromBody] string refreshToken)
        {
            var newAccessToken = await _jwtService.RenewAccessTokenAsync(refreshToken);

            return Ok(new { AccessToken = newAccessToken });
        }
    }
}