using FluentValidation;
using RecipeMicroservice.Application.Recipes.Commands.Create;
using RecipeMicroservice.Domain.Constants;

namespace RecipeMicroservice.Application.Validators
{
    public class CreateIngredientValidator : AbstractValidator<CreateIngredient>
    {
        public CreateIngredientValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage(ErrorMessages.IngredientNameIsRequired);

            RuleFor(x => x.Unit).NotEmpty().WithMessage(ErrorMessages.UnitIsRequired);
        }
    }
}