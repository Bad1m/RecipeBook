using MediatR;

namespace RecipeMicroservice.Application.Recipes.Commands.Delete
{
    public class DeleteIngredient : IRequest<bool>
    {
        public int Id { get; set; }
    }
}