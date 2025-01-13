using MediatR;
using RecipeMicroservice.Application.Recipes.Commands.Delete;
using RecipeMicroservice.Domain.Constants;
using RecipeMicroservice.Infrastructure.Interfaces;

namespace RecipeMicroservice.Application.Recipes.CommandHandlers.Delete
{
    public class DeleteInstructionHandler : IRequestHandler<DeleteInstructionCommand, bool>
    {
        private readonly IInstructionRepository _instructionRepository;

        private readonly ICacheRepository _cacheRepository;

        public DeleteInstructionHandler(IInstructionRepository instructionRepository, ICacheRepository cacheRepository)
        {
            _instructionRepository = instructionRepository;
            _cacheRepository = cacheRepository;
        }

        public async Task<bool> Handle(DeleteInstructionCommand request, CancellationToken cancellationToken)
        {
            var existingInstruction = await _instructionRepository.GetByIdAsync(request.Id, cancellationToken);

            if (existingInstruction == null)
            {
                throw new ArgumentNullException(ErrorMessages.InstructionNotFound);
            }

            await _instructionRepository.DeleteAsync(existingInstruction.Id, cancellationToken);
            await _cacheRepository.RemoveAsync(CacheKeys.Recipes);
            await _instructionRepository.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}