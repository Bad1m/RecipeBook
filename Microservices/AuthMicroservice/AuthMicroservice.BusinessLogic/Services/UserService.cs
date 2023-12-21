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

        public IEnumerable<UserDto> GetAllUsers(int page = 1, int pageSize = 10)
        {
            var users = _userManager.Users
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<UserDto?> GetUserDtoByLoginAsync(string login)
        {
            var user = await GetUserAsync(login);

            return _mapper.Map<UserDto>(user);
        }

        public async Task DeleteUserAsync(string login)
        {
            var user = await GetUserAsync(login);
            await _userManager.DeleteAsync(user);
        }

        private async Task<User> GetUserAsync(string login)
        {
            var user = await _userManager.FindByNameAsync(login);

            if (user == null)
            {
                throw new InvalidOperationException($"User with login '{login}' not found.");
            }

            return user;
        }
    }
}