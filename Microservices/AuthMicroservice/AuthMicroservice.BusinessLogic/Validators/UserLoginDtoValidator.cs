using AuthMicroservice.BusinessLogic.Dtos;
using FluentValidation;

namespace AuthMicroservice.BusinessLogic.Validators
{
    public class UserLoginDtoValidator : AbstractValidator<UserLoginDto>
    {
        public UserLoginDtoValidator()
        {
            RuleFor(userLoginDto => userLoginDto.UserName)
                .NotEmpty().WithMessage("Username is required");

            RuleFor(userLoginDto => userLoginDto.Password)
                .NotEmpty().WithMessage("Password is required");
        }
    }
}