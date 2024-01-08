using FluentValidation;
using RecipeMicroservice.Application.Recipes.Commands.Create;
using RecipeMicroservice.Domain.Constants;

namespace RecipeMicroservice.Application.Validators
{
    public class CreateInstructionForRecipeValidator : AbstractValidator<CreateInstructionForRecipe>
    {
        public CreateInstructionForRecipeValidator()
        {
            RuleFor(x => x.Description).NotEmpty().WithMessage(ErrorMessages.InstructionDescriptionIsRequired);

            RuleFor(x => x.StepNumber).NotEmpty().WithMessage(ErrorMessages.StepNumberIsRequired);

            RuleFor(x => x.RecipeId).NotEmpty().WithMessage(ErrorMessages.RecipeIdIsRequired);
        }
    }
}