using MediatR;
using RecipeMicroservice.Application.Helpers;
using RecipeMicroservice.Application.Recipes.Commands.Delete;
using RecipeMicroservice.Domain.Constants;
using RecipeMicroservice.Infrastructure.Interfaces;
using RecipeMicroservice.Infrastructure.Repositories;

namespace RecipeMicroservice.Application.Recipes.CommandHandlers.Delete
{
    public class DeleteRecipeHandler : IRequestHandler<DeleteRecipeCommand, bool>
    {
        private readonly IRecipeRepository _recipeRepository;

        private readonly RecipeExistenceChecker _recipeExistenceChecker;

        public DeleteRecipeHandler(IRecipeRepository recipeRepository, RecipeExistenceChecker recipeExistenceChecker)
        {
            _recipeRepository = recipeRepository;
            _recipeExistenceChecker = recipeExistenceChecker;
        }

        public async Task<bool> Handle(DeleteRecipeCommand request, CancellationToken cancellationToken)
        {
            var recipe = await _recipeExistenceChecker.CheckRecipeExistenceAsync(request.Id, cancellationToken);
            await _recipeRepository.DeleteAsync(recipe.Id, cancellationToken);
            await _recipeRepository.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}