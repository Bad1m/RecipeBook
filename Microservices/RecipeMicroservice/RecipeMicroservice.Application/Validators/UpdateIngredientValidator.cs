using FluentValidation;
using RecipeMicroservice.Application.Recipes.Commands.Update;
using RecipeMicroservice.Domain.Constants;

namespace RecipeMicroservice.Application.Validators
{
    public class UpdateIngredientValidator : AbstractValidator<UpdateIngredientCommand>
    {
        public UpdateIngredientValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage(ErrorMessages.IdIsRequired);

            RuleFor(x => x.Name).NotEmpty().WithMessage(ErrorMessages.IngredientNameIsRequired);

            RuleFor(x => x.Amount).NotEmpty().WithMessage(ErrorMessages.IngredientAmountIsRequired);
        }
    }
}