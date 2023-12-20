using AuthMicroservice.BusinessLogic.Dtos;
using AuthMicroservice.BusinessLogic.Helpers;
using AuthMicroservice.BusinessLogic.Interfaces;
using FluentValidation;

namespace AuthMicroservice.BusinessLogic.Validators
{
    public class UserLoginDtoValidator : AbstractValidator<UserLoginDto>, IUserLoginDtoValidator
    {
        public UserLoginDtoValidator()
        {
            RuleFor(userLoginDto => userLoginDto.UserName)
                .NotEmpty().WithMessage("Username is required");

            RuleFor(userLoginDto => userLoginDto.Password)
                .NotEmpty().WithMessage("Password is required");
        }

        public void ValidateUserLoginDto(UserLoginDto userLoginDto)
        {
            var validationResult = Validate(userLoginDto);
            ValidationHelper.ValidateAndThrow(validationResult);
        }
    }
}
