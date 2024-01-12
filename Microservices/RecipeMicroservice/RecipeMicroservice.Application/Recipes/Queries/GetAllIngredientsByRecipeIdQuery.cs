using MediatR;
using RecipeMicroservice.Application.Dtos;
using RecipeMicroservice.Domain.Settings;

namespace RecipeMicroservice.Application.Recipes.Queries
{
    public class GetAllIngredientsByRecipeIdQuery : IRequest<IEnumerable<IngredientDto>>
    {
        public int RecipeId { get; set; }
        public PaginationSettings PaginationSettings { get; set; }
    }
}