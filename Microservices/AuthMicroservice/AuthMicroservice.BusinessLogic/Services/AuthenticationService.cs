using AuthMicroservice.BusinessLogic.Dtos;
using AuthMicroservice.BusinessLogic.Interfaces;
using AuthMicroservice.BusinessLogic.Models;
using AuthMicroservice.DataAccess.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System.Security.Authentication;

namespace AuthMicroservice.BusinessLogic.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IJWTService _jwtService;
    
        public AuthenticationService(UserManager<User> userManager, IMapper mapper, IJWTService jwtService)
        {
            _userManager = userManager;
            _mapper = mapper;
            _jwtService = jwtService;
        }

        public async Task<User> RegisterUserAsync(UserRegistrationDto userRegistration)
        {
            var existingUser = await _userManager.FindByNameAsync(userRegistration.UserName);

            if (existingUser != null)
            {
                throw new InvalidOperationException("Username is already taken.");
            }

            var existingUserByEmail = await _userManager.FindByEmailAsync(userRegistration.Email);

            if (existingUserByEmail != null)
            {
                throw new InvalidOperationException("Email is already taken.");
            }

            var user = _mapper.Map<User>(userRegistration);
            var result = await _userManager.CreateAsync(user, userRegistration.Password);

            if (!result.Succeeded)
            {
                throw new InvalidOperationException("User registration failed.");
            }

            return user;
        }

        public async Task<TokenModel> LoginUserAsync(UserLoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.UserName);

            if (user != null && await _userManager.CheckPasswordAsync(user, loginDto.Password))
            {
                var tokenModel = await _jwtService.CreateTokenAsync(_mapper.Map<UserDto>(user));
                return tokenModel;
            }

            throw new AuthenticationException("Invalid credentials.");
        }
    }
}