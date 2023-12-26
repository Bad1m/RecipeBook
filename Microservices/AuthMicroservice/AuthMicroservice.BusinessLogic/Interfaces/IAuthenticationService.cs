using AuthMicroservice.BusinessLogic.Dtos;
using AuthMicroservice.BusinessLogic.Models;
using AuthMicroservice.DataAccess.Entities;

namespace AuthMicroservice.BusinessLogic.Interfaces
{
    public interface IAuthenticationService
    {
        Task<User> RegisterUserAsync(UserRegistrationDto userRegistration);
        Task<TokenModel> LoginUserAsync(UserLoginDto loginDto);
    }
}