using Microsoft.EntityFrameworkCore;
using RecipeMicroservice.Domain.Entities;
using RecipeMicroservice.Domain.Settings;
using RecipeMicroservice.Infrastructure.Data;
using RecipeMicroservice.Infrastructure.Interfaces;

namespace RecipeMicroservice.Infrastructure.Repositories
{
    public class IngredientRepository : Repository<Ingredient>, IIngredientRepository
    {
        public IngredientRepository(RecipeContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Ingredient>> GetIngredientsByRecipeIdAsync(int recipeId, PaginationSettings pagination, CancellationToken cancellationToken)
        {
            return await _dbSet.AsNoTracking()
                .Where(ingredient => ingredient.RecipeIngredients.Any(recipe => recipe.RecipeId == recipeId))
                .OrderBy(ingredient => ingredient.Id)
                .Skip((pagination.PageNumber - 1) * pagination.PageSize)
                .Take(pagination.PageSize)
                .ToListAsync(cancellationToken);
        }

        public async Task<Ingredient?> GetIngredientByRecipeIdAndNameAsync(int recipeId, string ingredientName, CancellationToken cancellationToken)
        {
            return await _dbSet.AsNoTracking()
                .FirstOrDefaultAsync(ingredient =>
                    ingredient.RecipeIngredients.Any(recipeIngredient => recipeIngredient.RecipeId == recipeId) &&
                    ingredient.Name == ingredientName,
                    cancellationToken);
        }

        public override async Task<Ingredient?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _dbSet.Include(i => i.RecipeIngredients)
                .AsNoTracking()
                .FirstOrDefaultAsync(i => i.Id == id, cancellationToken);
        }
    }
}