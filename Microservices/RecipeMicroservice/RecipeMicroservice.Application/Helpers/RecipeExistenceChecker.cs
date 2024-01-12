using RecipeMicroservice.Domain.Constants;
using RecipeMicroservice.Domain.Entities;
using RecipeMicroservice.Infrastructure.Interfaces;

namespace RecipeMicroservice.Application.Helpers
{
    public class RecipeExistenceChecker
    {
        private readonly IRecipeRepository _recipeRepository;

        public RecipeExistenceChecker(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        public async Task<Recipe> CheckRecipeExistenceAsync(int recipeId, CancellationToken cancellationToken)
        {
            var existingRecipe = await _recipeRepository.GetByIdAsync(recipeId, cancellationToken);

            if (existingRecipe == null)
            {
                throw new ArgumentNullException(ErrorMessages.RecipeNotFound);
            }

            return existingRecipe;
        }
    }
}