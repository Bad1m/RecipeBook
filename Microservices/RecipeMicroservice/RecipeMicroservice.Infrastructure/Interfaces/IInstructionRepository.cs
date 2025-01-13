using RecipeMicroservice.Domain.Entities;

namespace RecipeMicroservice.Infrastructure.Interfaces
{
    public interface IInstructionRepository : IRepository<Instruction>
    {
        Task<IEnumerable<Instruction>> GetInstructionsByRecipeIdAsync(int recipeId, CancellationToken cancellationToken);
        Task<bool> IsCheckDuplicateStepNumberAsync(int recipeId, int? stepNumber, int currentInstructionId, CancellationToken cancellationToken);
    }
}