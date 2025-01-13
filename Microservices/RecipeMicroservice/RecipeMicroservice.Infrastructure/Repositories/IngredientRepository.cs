using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RecipeMicroservice.Domain.Entities;
using RecipeMicroservice.Infrastructure.Data;
using RecipeMicroservice.Infrastructure.Interfaces;

namespace RecipeMicroservice.Infrastructure.Repositories
{
    public class IngredientRepository : Repository<Ingredient>, IIngredientRepository
    {
        public IngredientRepository(RecipeContext context) : base(context)
        {
        }

        public async Task<Ingredient> GetByIdWithRecipeIngredientsAsync(int id, CancellationToken cancellationToken)
        {
            return await _dbSet.Include(i => i.RecipeIngredients)
                               .FirstOrDefaultAsync(i => i.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<Ingredient>> GetIngredientsByRecipeIdAsync(int recipeId, CancellationToken cancellationToken)
        {
            return await _dbSet.AsNoTracking()
                .Where(ingredient => ingredient.RecipeIngredients.Any(recipe => recipe.RecipeId == recipeId))
                .OrderBy(ingredient => ingredient.Id)
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
            return await _dbSet.Include(ingredient => ingredient.RecipeIngredients)
                .AsNoTracking()
                .FirstOrDefaultAsync(ingredient => ingredient.Id == id, cancellationToken);
        }

        public virtual async Task UpdateAsync(Ingredient ingredient, CancellationToken cancellationToken)
        {
            var existingEntity = await _dbSet.FindAsync(ingredient.Id);
            if (existingEntity != null)
            {
                _context.Entry(existingEntity).State = EntityState.Detached;
            }

            _context.Entry(ingredient).State = EntityState.Modified;

            if (_context.Entry(ingredient).Navigations.Any(navigationEntry => navigationEntry is CollectionEntry))
            {
                foreach (var navigationEntry in _context.Entry(ingredient).Navigations)
                {
                    if (navigationEntry is CollectionEntry collectionEntry)
                    {
                        foreach (var relatedEntity in collectionEntry.CurrentValue)
                        {
                            _context.Entry(relatedEntity).State = EntityState.Modified;
                        }
                    }
                }
            }

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}