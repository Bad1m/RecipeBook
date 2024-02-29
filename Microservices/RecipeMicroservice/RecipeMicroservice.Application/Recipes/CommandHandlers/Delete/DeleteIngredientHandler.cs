using MediatR;
using RecipeMicroservice.Application.Recipes.Commands.Delete;
using RecipeMicroservice.Domain.Constants;
using RecipeMicroservice.Infrastructure.Interfaces;

namespace RecipeMicroservice.Application.Recipes.CommandHandlers.Delete
{
    public class DeleteIngredientHandler : IRequestHandler<DeleteIngredientCommand, bool>
    {
        private readonly IIngredientRepository _ingredientRepository;

        private readonly ICacheRepository _cacheRepository;

        public DeleteIngredientHandler(IIngredientRepository ingredientRepository, ICacheRepository cacheRepository)
        {
            _ingredientRepository = ingredientRepository;
            _cacheRepository = cacheRepository;
        }

        public async Task<bool> Handle(DeleteIngredientCommand request, CancellationToken cancellationToken)
        {
            var existingIngredient = await _ingredientRepository.GetByIdAsync(request.Id, cancellationToken);

            if (existingIngredient == null)
            {
                throw new ArgumentNullException(ErrorMessages.IngredientNotFound);
            }

            await _ingredientRepository.DeleteAsync(existingIngredient.Id, cancellationToken);
            await _cacheRepository.RemoveAsync(CacheKeys.Recipes);
            await _ingredientRepository.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}