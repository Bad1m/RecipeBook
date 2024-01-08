using MediatR;
using RecipeMicroservice.Application.Dtos;

namespace RecipeMicroservice.Application.Recipes.Commands.Create
{
    public class CreateRecipeIngredient : IRequest<RecipeIngredientDto>
    {
        public double Amount { get; set; }
        public CreateIngredient Ingredient { get; set; }
    }
}