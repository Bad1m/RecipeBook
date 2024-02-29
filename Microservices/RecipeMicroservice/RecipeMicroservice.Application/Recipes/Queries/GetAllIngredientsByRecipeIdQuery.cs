using MediatR;
using RecipeMicroservice.Application.Dtos;

namespace RecipeMicroservice.Application.Recipes.Queries
{
    public class GetAllIngredientsByRecipeIdQuery : IRequest<IEnumerable<IngredientDto>>
    {
        public int RecipeId { get; set; }
    }
}