using MediatR;
using RecipeMicroservice.Application.Dtos;

namespace RecipeMicroservice.Application.Recipes.Queries
{
    public class GetRecipeById : IRequest<RecipeDto>
    {
        public int Id { get; set; }
    }
}