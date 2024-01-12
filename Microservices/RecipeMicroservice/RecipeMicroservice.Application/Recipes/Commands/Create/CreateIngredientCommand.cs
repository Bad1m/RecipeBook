using MediatR;
using RecipeMicroservice.Application.Dtos;

namespace RecipeMicroservice.Application.Recipes.Commands.Create
{
    public class CreateIngredientCommand : IRequest<IngredientDto>
    {
        public string Name { get; set; }
        public string Unit { get; set; }
    }
}