using FluentValidation;
using RecipeMicroservice.Application.Recipes.Commands.Update;
using RecipeMicroservice.Domain.Constants;

namespace RecipeMicroservice.Application.Validators
{
    public class UpdateRecipeValidator : AbstractValidator<UpdateRecipeCommand>
    {
        public UpdateRecipeValidator()
        {
            RuleFor(x => x.Dish).NotEmpty().WithMessage(ErrorMessages.DishIsRequired);

            RuleFor(x => x.Category).NotEmpty().WithMessage(ErrorMessages.CategoryIsRequired);

            RuleFor(x => x.Description).NotEmpty().WithMessage(ErrorMessages.DescriptionIsRequired);

            RuleFor(x => x.PrepTime).NotEmpty().WithMessage(ErrorMessages.PrepTimeIsRequired);
        }
    }
}