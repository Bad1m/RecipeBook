using RecipeMicroservice.Domain.Entities;
using RecipeMicroservice.Domain.Settings;

namespace RecipeMicroservice.Infrastructure.Interfaces
{
    public interface IIngredientRepository : IRepository<Ingredient>
    {
        Task<Ingredient> GetByIdWithRecipeIngredientsAsync(int id, CancellationToken cancellationToken);
        Task<IEnumerable<Ingredient>> GetIngredientsByRecipeIdAsync(int recipeId, CancellationToken cancellationToken);
        Task<Ingredient?> GetIngredientByRecipeIdAndNameAsync(int recipeId, string ingredientName, CancellationToken cancellationToken);
    }
}