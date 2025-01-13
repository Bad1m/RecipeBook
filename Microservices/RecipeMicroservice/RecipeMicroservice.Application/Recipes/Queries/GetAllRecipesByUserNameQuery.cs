using MediatR;
using RecipeMicroservice.Application.Dtos;
using RecipeMicroservice.Domain.Models;
using RecipeMicroservice.Domain.Settings;

namespace RecipeMicroservice.Application.Recipes.Queries
{
    public class GetAllRecipesByUserNameQuery : IRequest<PaginatedResult<RecipeDto>>
    {
        public PaginationSettings PaginationSettings { get; set; }
        public string UserName { get; set; }
    }
}