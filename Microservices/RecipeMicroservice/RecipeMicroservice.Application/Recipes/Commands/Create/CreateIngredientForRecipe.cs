using MediatR;
using RecipeMicroservice.Application.Dtos;

namespace RecipeMicroservice.Application.Recipes.Commands.Create
{
    public class CreateIngredientForRecipe : IRequest<IngredientDto>
    {
        public string Name { get; set; }
        public string Unit { get; set; }
        public double Amount { get; set; }
        public int RecipeId { get; set; }
    }
}