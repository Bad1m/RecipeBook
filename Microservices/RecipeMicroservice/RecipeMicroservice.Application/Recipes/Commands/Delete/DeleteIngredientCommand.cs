using MediatR;

namespace RecipeMicroservice.Application.Recipes.Commands.Delete
{
    public class DeleteIngredientCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}