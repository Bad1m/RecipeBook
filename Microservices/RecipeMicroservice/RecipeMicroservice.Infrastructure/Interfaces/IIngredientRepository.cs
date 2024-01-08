using RecipeMicroservice.Domain.Entities;
using RecipeMicroservice.Domain.Settings;

namespace RecipeMicroservice.Infrastructure.Interfaces
{
    public interface IIngredientRepository : IRepository<Ingredient>
    {
        Task<IEnumerable<Ingredient>> GetIngredientsByRecipeIdAsync(int recipeId, PaginationSettings pagination, CancellationToken cancellationToken);
    }
}