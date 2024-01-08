using Microsoft.EntityFrameworkCore;
using RecipeMicroservice.Domain.Entities;
using RecipeMicroservice.Domain.Settings;
using RecipeMicroservice.Infrastructure.Data;
using RecipeMicroservice.Infrastructure.Interfaces;

namespace RecipeMicroservice.Infrastructure.Repositories
{
    public class RecipeRepository : Repository<Recipe>, IRecipeRepository
    {
        public RecipeRepository(RecipeContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<Recipe>> GetAllAsync(PaginationSettings pagination, CancellationToken cancellationToken)
        {
            return await _dbSet.AsNoTracking()
                .Include(recipe => recipe.RecipeIngredients)
                    .ThenInclude(recipe => recipe.Ingredient)
                .Include(recipe => recipe.Instructions)
                .OrderBy(recipe => recipe.Id)
                .Skip((pagination.PageNumber - 1) * pagination.PageSize)
                .Take(pagination.PageSize)
                .ToListAsync(cancellationToken);
        }

        public override async Task<Recipe> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _dbSet.AsNoTracking()
                .Include(recipe => recipe.RecipeIngredients)
                    .ThenInclude(recipe => recipe.Ingredient)
                .Include(recipe => recipe.Instructions)
                .FirstOrDefaultAsync(recipe => recipe.Id == id, cancellationToken);
        }

        public override async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var recipe = await _dbSet
                .Include(recipe => recipe.RecipeIngredients)
                .Include(recipe => recipe.Instructions)
                .FirstOrDefaultAsync(recipe => recipe.Id == id, cancellationToken);

            if (recipe != null)
            {
                _context.Set<RecipeIngredient>().RemoveRange(recipe.RecipeIngredients);
                _context.Set<Instruction>().RemoveRange(recipe.Instructions);
                _dbSet.Remove(recipe);
            }
        }
    }
}