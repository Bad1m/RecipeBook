using FluentValidation;
using RecipeMicroservice.Application.Recipes.Commands.Create;
using RecipeMicroservice.Domain.Constants;

namespace RecipeMicroservice.Application.Validators
{
    public class CreateInstructionValidator : AbstractValidator<CreateInstructionCommand>
    {
        public CreateInstructionValidator()
        {
            RuleFor(x => x.StepNumber).NotEmpty().WithMessage(ErrorMessages.StepNumberIsRequired);

            RuleFor(x => x.Description).NotEmpty().WithMessage(ErrorMessages.DescriptionIsRequired);
        }
    }
}