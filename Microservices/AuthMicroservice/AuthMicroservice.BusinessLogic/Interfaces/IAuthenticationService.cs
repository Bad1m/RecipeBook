using AuthMicroservice.BusinessLogic.Dtos;
using AuthMicroservice.BusinessLogic.Models;

namespace AuthMicroservice.BusinessLogic.Interfaces
{
    public interface IAuthenticationService
    {
        Task<UserDto> RegisterUserAsync(UserRegistrationDto userRegistration);
        Task<TokenModel> LoginUserAsync(UserLoginDto loginDto);
    }
}