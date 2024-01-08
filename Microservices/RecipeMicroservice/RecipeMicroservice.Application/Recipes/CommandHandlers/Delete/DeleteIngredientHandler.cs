using MediatR;
using RecipeMicroservice.Application.Recipes.Commands.Delete;
using RecipeMicroservice.Domain.Constants;
using RecipeMicroservice.Infrastructure.Interfaces;

namespace RecipeMicroservice.Application.Recipes.CommandHandlers.Delete
{
    public class DeleteIngredientHandler : IRequestHandler<DeleteIngredient, bool>
    {
        private readonly IIngredientRepository _ingredientRepository;

        public DeleteIngredientHandler(IIngredientRepository ingredientRepository)
        {
            _ingredientRepository = ingredientRepository;
        }

        public async Task<bool> Handle(DeleteIngredient request, CancellationToken cancellationToken)
        {
            var existingIngredient = await _ingredientRepository.GetByIdAsync(request.Id, cancellationToken);

            if (existingIngredient == null)
            {
                throw new ArgumentNullException(ErrorMessages.IngredientNotFound);
            }

            await _ingredientRepository.DeleteAsync(existingIngredient.Id, cancellationToken);
            await _ingredientRepository.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}