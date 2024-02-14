using MongoDB.Driver;
using ReviewMicroservice.Domain.Entities;
using ReviewMicroservice.Infrastructure.Data;
using ReviewMicroservice.Infrastructure.Interfaces;

namespace ReviewMicroservice.Infrastructure.Repositories
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly MongoDBContext _context;

        public RecipeRepository(MongoDBContext context)
        {
            _context = context;
        }

        public async Task DeleteByIdAsync(int id, CancellationToken cancellationToken)
        {
            await _context.Recipes.DeleteOneAsync(recipe => recipe.RecipeId == id, cancellationToken);
        }

        public async Task<Recipe> GetByIdAsync(int id)
        {
            return await _context.Recipes.Find(recipe => recipe.RecipeId == id).FirstOrDefaultAsync();
        }

        public async Task InsertAsync(Recipe recipe)
        {
            await _context.Recipes.InsertOneAsync(recipe);
        }

        public async Task UpdateAsync(int id, Recipe updatedRecipe)
        {
            var filter = Builders<Recipe>.Filter.Eq(r => r.RecipeId, id);
            await _context.Recipes.ReplaceOneAsync(filter, updatedRecipe);
        }
    }
}