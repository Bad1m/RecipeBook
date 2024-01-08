using MediatR;
using RecipeMicroservice.Application.Dtos;

namespace RecipeMicroservice.Application.Recipes.Queries
{
    public class GetAllIngredientsByRecipeId : IRequest<IEnumerable<IngredientDto>>
    {
        public int RecipeId { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}