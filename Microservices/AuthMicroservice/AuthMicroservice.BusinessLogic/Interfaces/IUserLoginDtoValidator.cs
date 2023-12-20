using AuthMicroservice.BusinessLogic.Dtos;

namespace AuthMicroservice.BusinessLogic.Interfaces
{
    public interface IUserLoginDtoValidator
    {
        void ValidateUserLoginDto(UserLoginDto userLoginDto);
    }
}