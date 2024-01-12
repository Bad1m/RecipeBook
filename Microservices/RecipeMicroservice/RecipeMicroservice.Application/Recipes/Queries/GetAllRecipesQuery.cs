using MediatR;
using RecipeMicroservice.Application.Dtos;
using RecipeMicroservice.Domain.Settings;

namespace RecipeMicroservice.Application.Recipes.Queries
{
    public class GetAllRecipesQuery : IRequest<IEnumerable<RecipeDto>>
    {
        public PaginationSettings PaginationSettings { get; set; }
    }
}