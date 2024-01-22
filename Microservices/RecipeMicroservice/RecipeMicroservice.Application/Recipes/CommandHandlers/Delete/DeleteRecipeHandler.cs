using MediatR;
using RecipeMicroservice.Application.Interfaces;
using RecipeMicroservice.Application.Messages;
using RecipeMicroservice.Application.Recipes.Commands.Delete;
using RecipeMicroservice.Infrastructure.Interfaces;

namespace RecipeMicroservice.Application.Recipes.CommandHandlers.Delete
{
    public class DeleteRecipeHandler : IRequestHandler<DeleteRecipeCommand, bool>
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IRecipeExistenceChecker _recipeExistenceChecker;
        private readonly IRabbitMqProducer _rabbitMqProducer;

        public DeleteRecipeHandler(IRecipeRepository recipeRepository, IRecipeExistenceChecker recipeExistenceChecker, IRabbitMqProducer rabbitMqProducer)
        {
            _recipeRepository = recipeRepository;
            _recipeExistenceChecker = recipeExistenceChecker;
            _rabbitMqProducer = rabbitMqProducer;
        }

        public async Task<bool> Handle(DeleteRecipeCommand request, CancellationToken cancellationToken)
        {
            var recipe = await _recipeExistenceChecker.CheckRecipeExistenceAsync(request.Id, cancellationToken);
            await _recipeRepository.DeleteAsync(recipe.Id, cancellationToken);
            await _recipeRepository.SaveChangesAsync(cancellationToken);
            _rabbitMqProducer.SendMessage(new RecipeDeletedMessage
            {
                RecipeId = request.Id
            });

            return true;
        }
    }
}