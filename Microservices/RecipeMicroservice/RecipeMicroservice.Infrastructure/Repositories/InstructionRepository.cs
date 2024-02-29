using Microsoft.EntityFrameworkCore;
using RecipeMicroservice.Domain.Entities;
using RecipeMicroservice.Infrastructure.Data;
using RecipeMicroservice.Infrastructure.Interfaces;

namespace RecipeMicroservice.Infrastructure.Repositories
{
    public class InstructionRepository : Repository<Instruction>, IInstructionRepository
    {
        public InstructionRepository(RecipeContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Instruction>> GetInstructionsByRecipeIdAsync(int recipeId, CancellationToken cancellationToken)
        {
            return await _dbSet.AsNoTracking()
                .Where(instruction => instruction.RecipeId == recipeId)
                .OrderBy(instruction => instruction.StepNumber)
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> IsCheckDuplicateStepNumberAsync(int recipeId, int? stepNumber, int currentInstructionId, CancellationToken cancellationToken)
        {
            return await _dbSet.AsNoTracking()
                .AnyAsync(instruction =>
                    instruction.RecipeId == recipeId &&
                    instruction.StepNumber == stepNumber &&
                    instruction.Id != currentInstructionId, cancellationToken);
        }
    }
}