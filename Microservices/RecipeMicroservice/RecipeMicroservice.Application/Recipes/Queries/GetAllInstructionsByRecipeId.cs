using MediatR;
using RecipeMicroservice.Application.Dtos;

namespace RecipeMicroservice.Application.Recipes.Queries
{
    public class GetAllInstructionsByRecipeId : IRequest<IEnumerable<InstructionDto>>
    {
        public int RecipeId { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}