using MediatR;
using RecipeMicroservice.Application.Interfaces;
using RecipeMicroservice.Application.Recipes.Commands.Delete;
using RecipeMicroservice.Infrastructure.Interfaces;

namespace RecipeMicroservice.Application.Recipes.CommandHandlers.Delete
{
    public class DeleteRecipeHandler : IRequestHandler<DeleteRecipeCommand, bool>
    {
        private readonly IRecipeRepository _recipeRepository;

        private readonly IRecipeExistenceChecker _recipeExistenceChecker;

        public DeleteRecipeHandler(IRecipeRepository recipeRepository, IRecipeExistenceChecker recipeExistenceChecker)
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