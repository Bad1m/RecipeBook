using AuthMicroservice.BusinessLogic.Dtos;
using AuthMicroservice.BusinessLogic.Models;
using Microsoft.AspNetCore.Identity;

namespace AuthMicroservice.BusinessLogic.Interfaces
{
    public interface IAuthenticationService
    {
        Task<IdentityResult> RegisterUserAsync(UserRegistrationDto userForRegistration);
        Task<IdentityResult> RegisterAdminAsync(UserRegistrationDto adminRegistration);
        Task<TokenModel> LoginUserAsync(UserLoginDto loginDto);
    }
}