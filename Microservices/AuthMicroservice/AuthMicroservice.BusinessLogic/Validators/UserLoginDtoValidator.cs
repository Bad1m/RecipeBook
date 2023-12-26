using AuthMicroservice.BusinessLogic.Constants;
using AuthMicroservice.BusinessLogic.Dtos;
using FluentValidation;

namespace AuthMicroservice.BusinessLogic.Validators
{
    public class UserLoginDtoValidator : AbstractValidator<UserLoginDto>
    {
        public UserLoginDtoValidator()
        {
            RuleFor(userLoginDto => userLoginDto.UserName)
                .NotEmpty().WithMessage(ErrorMessages.UsernameRequired);

            RuleFor(userLoginDto => userLoginDto.Password)
                .NotEmpty().WithMessage(ErrorMessages.PasswordRequired);
        }
    }
}