using Microsoft.EntityFrameworkCore;
using RecipeMicroservice.Domain.Constants;
using RecipeMicroservice.Domain.Entities;
using RecipeMicroservice.Domain.Models;
using RecipeMicroservice.Domain.Settings;
using RecipeMicroservice.Infrastructure.Data;
using RecipeMicroservice.Infrastructure.Interfaces;

namespace RecipeMicroservice.Infrastructure.Repositories
{
    public class RecipeRepository : Repository<Recipe>, IRecipeRepository
    {
        private ICacheRepository _cacheRepository;

        public RecipeRepository(RecipeContext context, ICacheRepository cacheRepository) : base(context)
        {
            _cacheRepository = cacheRepository;
        }

        public new async Task<PaginatedResult<Recipe>> GetAllAsync(PaginationSettings pagination, CancellationToken cancellationToken)
        {
            var cacheKey = CacheKeys.Recipes;

            var allRecipes = await _cacheRepository.GetDataAsync<List<Recipe>>(cacheKey);

            if (allRecipes == null)
            {
                var query = _dbSet.AsNoTracking()
                    .Include(recipe => recipe.RecipeIngredients)
                        .ThenInclude(recipe => recipe.Ingredient)
                    .Include(recipe => recipe.Instructions)
                    .Include(recipe => recipe.User);

                allRecipes = await query.OrderBy(recipe => recipe.Id).ToListAsync(cancellationToken);
                await _cacheRepository.SetDataAsync(cacheKey, allRecipes);
            }

            var totalCount = allRecipes.Count;
            var paginatedData = allRecipes
                .Skip((pagination.PageNumber - 1) * pagination.PageSize)
                .Take(pagination.PageSize)
                .ToList();

            return new PaginatedResult<Recipe>
            {
                Data = paginatedData,
                TotalCount = totalCount
            };
        }

        public async Task<PaginatedResult<Recipe>> GetRecipesByUserNameAsync(string userName, PaginationSettings paginationSettings, CancellationToken cancellationToken)
        {
            var query = _dbSet.AsNoTracking()
                .Include(recipe => recipe.RecipeIngredients)
                    .ThenInclude(recipe => recipe.Ingredient)
                .Include(recipe => recipe.Instructions)
                .Where(recipe => recipe.User != null && recipe.User.UserName == userName);

            var totalCount = await query.CountAsync(cancellationToken);

            var pagedRecipes = await query
                .OrderBy(recipe => recipe.Id)
                .Skip((paginationSettings.PageNumber - 1) * paginationSettings.PageSize)
                .Take(paginationSettings.PageSize)
                .ToListAsync(cancellationToken);

            return new PaginatedResult<Recipe>
            {
                Data = pagedRecipes,
                TotalCount = totalCount
            };
        }

        public override async Task<Recipe> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _dbSet.AsNoTracking()
                .Include(recipe => recipe.RecipeIngredients)
                    .ThenInclude(recipe => recipe.Ingredient)
                .Include(recipe => recipe.Instructions)
                .Include(recipe => recipe.User)
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
                _dbSet.Remove(recipe);
            }
        }
    }
}