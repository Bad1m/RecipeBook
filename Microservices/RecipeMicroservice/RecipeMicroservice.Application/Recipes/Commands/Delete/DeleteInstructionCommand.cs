using MediatR;

namespace RecipeMicroservice.Application.Recipes.Commands.Delete
{
    public class DeleteInstructionCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}