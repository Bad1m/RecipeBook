using AuthMicroservice.BusinessLogic.Constants;
using AuthMicroservice.BusinessLogic.Dtos;
using FluentValidation;

namespace AuthMicroservice.BusinessLogic.Validators
{
    public class UserRegistrationDtoValidator : AbstractValidator<UserRegistrationDto>
    {
        public UserRegistrationDtoValidator()
        {
            RuleFor(userDto => userDto.FirstName)
                .NotEmpty().WithMessage(ErrorMessages.FirstNameRequired);

            RuleFor(userDto => userDto.LastName)
                .NotEmpty().WithMessage(ErrorMessages.LastNameRequired);

            RuleFor(userDto => userDto.UserName)
                .NotEmpty().WithMessage(ErrorMessages.UsernameRequired);

            RuleFor(userDto => userDto.Password)
                .NotEmpty().WithMessage(ErrorMessages.PasswordRequired);

            RuleFor(userDto => userDto.Email)
                .NotEmpty().WithMessage(ErrorMessages.EmailRequired)
                .EmailAddress().WithMessage(ErrorMessages.InvalidEmail);
        }
    }
}