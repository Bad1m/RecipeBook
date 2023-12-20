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
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly IUserLoginDtoValidator _userLoginDtoValidator;
        private readonly IUserRegistrationDtoValidator _userRegistrationDtoValidator;
        private readonly IJWTService _jwtService;
    
        public AuthenticationService(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper, IUserLoginDtoValidator userLoginDtoValidator, IUserRegistrationDtoValidator userRegistrationDtoValidator, IJWTService jwtService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _userLoginDtoValidator = userLoginDtoValidator;
            _userRegistrationDtoValidator = userRegistrationDtoValidator;
            _jwtService = jwtService;
        }

        public async Task<IdentityResult> RegisterUserAsync(UserRegistrationDto userRegistration)
        {
            return await RegisterUserWithRoleAsync(userRegistration, "user");
        }

        public async Task<IdentityResult> RegisterAdminAsync(UserRegistrationDto adminRegistration)
        {
            return await RegisterUserWithRoleAsync(adminRegistration, "admin");
        }

        private async Task<IdentityResult> RegisterUserWithRoleAsync(UserRegistrationDto userRegistration, string role)
        {
            _userRegistrationDtoValidator.ValidaterRegistrationDto(userRegistration);
            var roleExists = await _roleManager.RoleExistsAsync(role);

            if (!roleExists)
            {
                await _roleManager.CreateAsync(new IdentityRole(role));
            }

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
            await _userManager.AddToRoleAsync(user, role);

            if (!result.Succeeded)
            {
                throw new InvalidOperationException("User registration failed.");
            }

            return result;
        }

        public async Task<TokenModel> LoginUserAsync(UserLoginDto loginDto)
        {
            _userLoginDtoValidator.ValidateUserLoginDto(loginDto);
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