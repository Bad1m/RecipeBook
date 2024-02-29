using MediatR;
using RecipeMicroservice.Application.Dtos;
using RecipeMicroservice.Domain.Settings;

namespace RecipeMicroservice.Application.Recipes.Queries
{
    public class GetAllInstructionsByRecipeIdQuery : IRequest<IEnumerable<InstructionDto>>
    {
        public int RecipeId { get; set; }
    }
}