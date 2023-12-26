using AuthMicroservice.BusinessLogic.Constants;
using AuthMicroservice.BusinessLogic.Dtos;
using AuthMicroservice.BusinessLogic.Interfaces;
using AuthMicroservice.BusinessLogic.Models;
using AuthMicroservice.DataAccess.Entities;
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
                throw new InvalidOperationException(ErrorMessages.UsernameAlreadyTaken);
            }

            var existingUserByEmail = await _userManager.FindByEmailAsync(userRegistration.Email);

            if (existingUserByEmail != null)
            {
                throw new InvalidOperationException(ErrorMessages.EmailAlreadyTaken);
            }

            var user = _mapper.Map<User>(userRegistration);
            var result = await _userManager.CreateAsync(user, userRegistration.Password);

            if (!result.Succeeded)
            {
                throw new InvalidOperationException(ErrorMessages.UserRegistrationFailed);
            }

            return user;
        }

        public async Task<TokenModel> LoginUserAsync(UserLoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.UserName);
            var isCorrectPassword = await _userManager.CheckPasswordAsync(user, loginDto.Password);

            if (user != null && isCorrectPassword)
            {
                var tokenModel = await _jwtService.CreateTokenAsync(_mapper.Map<UserDto>(user));

                return tokenModel;
            }

            throw new AuthenticationException(ErrorMessages.InvalidCredentials);
        }
    }
}