using MediatR;

namespace RecipeMicroservice.Application.Recipes.Commands.Delete
{
    public class DeleteInstruction : IRequest<bool>
    {
        public int Id { get; set; }
    }
}