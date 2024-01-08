using MediatR;
using RecipeMicroservice.Application.Recipes.Commands.Delete;
using RecipeMicroservice.Domain.Constants;
using RecipeMicroservice.Infrastructure.Interfaces;

namespace RecipeMicroservice.Application.Recipes.CommandHandlers.Delete
{
    public class DeleteRecipeHandler : IRequestHandler<DeleteRecipe, bool>
    {
        private readonly IRecipeRepository _recipeRepository;

        public DeleteRecipeHandler(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        public async Task<bool> Handle(DeleteRecipe request, CancellationToken cancellationToken)
        {
            var existingRecipe = await _recipeRepository.GetByIdAsync(request.Id, cancellationToken);

            if (existingRecipe == null)
            {
                throw new ArgumentNullException(ErrorMessages.RecipeNotFound);
            }

            await _recipeRepository.DeleteAsync(existingRecipe.Id, cancellationToken);
            await _recipeRepository.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}