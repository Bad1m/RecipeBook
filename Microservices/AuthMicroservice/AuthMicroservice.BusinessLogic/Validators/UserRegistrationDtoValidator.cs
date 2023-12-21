using AuthMicroservice.BusinessLogic.Dtos;
using FluentValidation;

namespace AuthMicroservice.BusinessLogic.Validators
{
    public class UserRegistrationDtoValidator : AbstractValidator<UserRegistrationDto>
    {
        public UserRegistrationDtoValidator()
        {
            RuleFor(userDto => userDto.FirstName)
                .NotEmpty().WithMessage("First name is required");

            RuleFor(userDto => userDto.LastName)
                .NotEmpty().WithMessage("Last name is required");

            RuleFor(userDto => userDto.UserName)
                .NotEmpty().WithMessage("Username is required");

            RuleFor(userDto => userDto.Password)
                .NotEmpty().WithMessage("Password is required");

            RuleFor(userDto => userDto.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email address");
        }
    }
}