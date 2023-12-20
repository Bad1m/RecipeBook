using AuthMicroservice.BusinessLogic.Dtos;

namespace AuthMicroservice.BusinessLogic.Interfaces
{
    public interface IUserRegistrationDtoValidator
    {
        void ValidaterRegistrationDto(UserRegistrationDto userRegistrationDto);
    }
}