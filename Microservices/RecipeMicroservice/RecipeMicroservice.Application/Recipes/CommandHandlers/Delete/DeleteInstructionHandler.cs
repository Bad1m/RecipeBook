using MediatR;
using RecipeMicroservice.Application.Recipes.Commands.Delete;
using RecipeMicroservice.Domain.Constants;
using RecipeMicroservice.Infrastructure.Interfaces;

namespace RecipeMicroservice.Application.Recipes.CommandHandlers.Delete
{
    public class DeleteInstructionHandler : IRequestHandler<DeleteInstruction, bool>
    {
        private readonly IInstructionRepository _instructionRepository;

        public DeleteInstructionHandler(IInstructionRepository instructionRepository)
        {
            _instructionRepository = instructionRepository;
        }

        public async Task<bool> Handle(DeleteInstruction request, CancellationToken cancellationToken)
        {
            var existingInstruction = await _instructionRepository.GetByIdAsync(request.Id, cancellationToken);

            if (existingInstruction == null)
            {
                throw new ArgumentNullException(ErrorMessages.InstructionNotFound);
            }

            await _instructionRepository.DeleteAsync(existingInstruction.Id, cancellationToken);
            await _instructionRepository.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}