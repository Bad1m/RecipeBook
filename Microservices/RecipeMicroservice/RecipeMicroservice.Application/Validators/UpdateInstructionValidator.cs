using FluentValidation;
using RecipeMicroservice.Application.Recipes.Commands.Update;
using RecipeMicroservice.Domain.Constants;

namespace RecipeMicroservice.Application.Validators
{
    public class UpdateInstructionValidator : AbstractValidator<UpdateInstructionCommand>
    {
        public UpdateInstructionValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage(ErrorMessages.IdIsRequired);

            RuleFor(x => x.StepNumber).NotEmpty().WithMessage(ErrorMessages.StepNumberIsRequired);

            RuleFor(x => x.Description).NotEmpty().WithMessage(ErrorMessages.InstructionDescriptionIsRequired);
        }
    }
}