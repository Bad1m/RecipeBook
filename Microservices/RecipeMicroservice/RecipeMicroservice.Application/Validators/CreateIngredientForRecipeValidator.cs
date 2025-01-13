using FluentValidation;
using RecipeMicroservice.Application.Recipes.Commands.Create;
using RecipeMicroservice.Domain.Constants;

namespace RecipeMicroservice.Application.Validators
{
    public class CreateIngredientForRecipeValidator : AbstractValidator<CreateIngredientForRecipeCommand>
    {
        public CreateIngredientForRecipeValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage(ErrorMessages.IngredientNameIsRequired);

            RuleFor(x => x.Unit).NotEmpty().WithMessage(ErrorMessages.UnitIsRequired);

            RuleFor(x => x.RecipeId).NotEmpty().WithMessage(ErrorMessages.RecipeIdIsRequired);
        }
    }
}