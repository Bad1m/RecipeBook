using MediatR;
using RecipeMicroservice.Application.Dtos;
using RecipeMicroservice.Domain.Models;
using RecipeMicroservice.Domain.Settings;

namespace RecipeMicroservice.Application.Recipes.Queries
{
    public class GetAllRecipesQuery : IRequest<PaginatedResult<RecipeDto>>
    {
        public PaginationSettings PaginationSettings { get; set; }
    }
}