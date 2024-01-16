using RecipeMicroservice.Domain.Entities;

namespace RecipeMicroservice.Application.Interfaces
{
    public interface IRecipeExistenceChecker
    {
        Task<Recipe> CheckRecipeExistenceAsync(int recipeId, CancellationToken cancellationToken);
    }
}