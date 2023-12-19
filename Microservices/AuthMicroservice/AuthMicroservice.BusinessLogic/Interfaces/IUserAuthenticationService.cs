using AuthMicroservice.BusinessLogic.Dtos;
using AuthMicroservice.DataAccess.Models;
using Microsoft.AspNetCore.Identity;

namespace AuthMicroservice.BusinessLogic.Interfaces
{
    public interface IUserAuthenticationService
    {
        Task<IdentityResult> RegisterUserAsync(UserRegistrationDto userForRegistration);
        Task<bool> IsValidUserAsync(UserLoginDto loginDto);
        Task<User?> GetUserByLoginAsync(string userId);
    }
}