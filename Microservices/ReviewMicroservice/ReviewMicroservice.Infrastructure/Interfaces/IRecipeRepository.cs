using MongoDB.Driver;
using ReviewMicroservice.Domain.Entities;

namespace ReviewMicroservice.Infrastructure.Interfaces
{
    public interface IRecipeRepository
    {
        Task DeleteByIdAsync(int id, CancellationToken cancellationToken);

        Task<Recipe> GetByIdAsync(int id);

        Task InsertAsync(Recipe recipe);

        Task UpdateAsync(int id, Recipe updatedRecipe);
    }
}