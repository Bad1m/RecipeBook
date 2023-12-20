using AuthMicroservice.BusinessLogic.Dtos;
using AuthMicroservice.BusinessLogic.Interfaces;
using AuthMicroservice.DataAccess.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace AuthMicroservice.BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public UserService(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public IEnumerable<UserDto> GetAllUsers()
        {
            var users = _userManager.Users.ToList();

            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<UserDto?> GetUserByLoginAsync(string login)
        {
            var user = await _userManager.FindByNameAsync(login);

            if (user == null)
            {
                throw new InvalidOperationException($"User with login '{login}' not found.");
            }

            return _mapper.Map<UserDto>(user);
        }

        public async Task DeleteUserAsync(string login)
        {
            var user = await GetUserByLoginAsync(login);
            await _userManager.DeleteAsync(_mapper.Map<User>(user));
        }
    }
}