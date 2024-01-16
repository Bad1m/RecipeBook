using MediatR;
using RecipeMicroservice.Application.Dtos;

namespace RecipeMicroservice.Application.Recipes.Commands.Update
{
    public class UpdateIngredientCommand : IRequest<IngredientDto>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
    }
}