using MediatR;

namespace RecipeMicroservice.Application.Recipes.Commands.Delete
{
    public class DeleteRecipeCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}