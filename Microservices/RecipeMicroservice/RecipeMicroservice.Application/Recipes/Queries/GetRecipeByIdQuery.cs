using MediatR;
using RecipeMicroservice.Application.Dtos;

namespace RecipeMicroservice.Application.Recipes.Queries
{
    public class GetRecipeByIdQuery : IRequest<RecipeDto>
    {
        public int Id { get; set; }
    }
}