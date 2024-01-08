using MediatR;
using RecipeMicroservice.Application.Dtos;

namespace RecipeMicroservice.Application.Recipes.Commands.Create
{
    public class CreateIngredient : IRequest<IngredientDto>
    {
        public string Name { get; set; }
        public string Unit { get; set; }
    }
}