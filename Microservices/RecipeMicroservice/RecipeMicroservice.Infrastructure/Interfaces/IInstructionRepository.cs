using RecipeMicroservice.Domain.Entities;
using RecipeMicroservice.Domain.Settings;

namespace RecipeMicroservice.Infrastructure.Interfaces
{
    public interface IInstructionRepository : IRepository<Instruction>
    {
        Task<IEnumerable<Instruction>> GetInstructionsByRecipeIdAsync(int recipeId, PaginationSettings pagination, CancellationToken cancellationToken);
        Task<bool> CheckDuplicateStepNumberAsync(int recipeId, int? stepNumber, CancellationToken cancellationToken);
    }
}