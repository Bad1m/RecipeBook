using MediatR;
using RecipeMicroservice.Application.Dtos;

namespace RecipeMicroservice.Application.Recipes.Queries
{
    public class GetAllRecipes : IRequest<IEnumerable<RecipeDto>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}