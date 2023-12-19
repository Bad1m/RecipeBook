using AuthMicroservice.BusinessLogic.Dtos;
using AuthMicroservice.BusinessLogic.Interfaces;
using AuthMicroservice.DataAccess.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace AuthMicroservice.BusinessLogic.Services
{
    public class UserAuthenticationService : IUserAuthenticationService
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IUserContext _userContext;
    
        public UserAuthenticationService(UserManager<User> userManager, IMapper mapper, IUserContext userContext)
        {
            _userManager = userManager;
            _mapper = mapper;
            _userContext = userContext;
        }

        public async Task<IdentityResult> RegisterUserAsync(UserRegistrationDto userRegistration)
        {
            var user = _mapper.Map<User>(userRegistration);
            var result = await _userManager.CreateAsync(user, userRegistration.Password);
            return result;
        }

        public async Task<bool> IsValidUserAsync(UserLoginDto loginDto)
        {
            var _user = await GetUserByLoginAsync(loginDto.UserName);
            _userContext.CurrentUser = _user;
            var result = _user != null && await _userManager.CheckPasswordAsync(_user, loginDto.Password);
            return result;
        }

        public async Task<User?> GetUserByLoginAsync(string login)
        {
            var user = await _userManager.FindByNameAsync(login);
            return user;
        }
    }
}