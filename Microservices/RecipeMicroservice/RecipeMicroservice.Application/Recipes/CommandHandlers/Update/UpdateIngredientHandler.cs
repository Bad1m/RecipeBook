using AutoMapper;
using MediatR;
using RecipeMicroservice.Application.Dtos;
using RecipeMicroservice.Application.Recipes.Commands.Update;
using RecipeMicroservice.Domain.Constants;
using RecipeMicroservice.Infrastructure.Interfaces;

namespace RecipeMicroservice.Application.Recipes.CommandHandlers.Update
{
    public class UpdateIngredientHandler : IRequestHandler<UpdateIngredientCommand, IngredientDto>
    {
        private readonly IIngredientRepository _ingredientRepository;

        private readonly IMapper _mapper;

        private readonly ICacheRepository _cacheRepository;

        public UpdateIngredientHandler(IIngredientRepository ingredientRepository, IMapper mapper, ICacheRepository cacheRepository)
        {
            _ingredientRepository = ingredientRepository;
            _mapper = mapper;
            _cacheRepository = cacheRepository;
        }

        public async Task<IngredientDto> Handle(UpdateIngredientCommand request, CancellationToken cancellationToken)
        {
            var existingIngredient = await _ingredientRepository.GetByIdWithRecipeIngredientsAsync(request.Id, cancellationToken);

            if (existingIngredient == null)
            {
                throw new ArgumentNullException(ErrorMessages.IngredientNotFound);
            }

            _mapper.Map(request, existingIngredient);
            var recipeIngredient = existingIngredient.RecipeIngredients?.FirstOrDefault();

            if (recipeIngredient != null)
            {
                recipeIngredient.Amount = request.Amount;
            }

            await _ingredientRepository.UpdateAsync(existingIngredient, cancellationToken);
            await _cacheRepository.RemoveAsync(CacheKeys.Recipes);
            var updatedIngredientDto = _mapper.Map<IngredientDto>(existingIngredient);

            return updatedIngredientDto;
        }
    }
}