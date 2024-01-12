using AutoMapper;
using MediatR;
using RecipeMicroservice.Application.Dtos;
using RecipeMicroservice.Application.Helpers;
using RecipeMicroservice.Application.Recipes.Commands.Create;
using RecipeMicroservice.Domain.Constants;
using RecipeMicroservice.Domain.Entities;
using RecipeMicroservice.Infrastructure.Interfaces;

namespace RecipeMicroservice.Application.Recipes.CommandHandlers.Create
{
    public class CreateIngredientHandler : IRequestHandler<CreateIngredientForRecipeCommand, IngredientDto>
    {
        private readonly IIngredientRepository _ingredientRepository;

        private readonly IMapper _mapper;

        private readonly IRecipeRepository _recipeRepository;

        private readonly RecipeExistenceChecker _recipeExistenceChecker;

        public CreateIngredientHandler(IIngredientRepository ingredientRepository, IMapper mapper, IRecipeRepository recipeRepository, RecipeExistenceChecker recipeExistenceChecker)
        {
            _ingredientRepository = ingredientRepository;
            _mapper = mapper;
            _recipeRepository = recipeRepository;
            _recipeExistenceChecker = recipeExistenceChecker;
        }

        public async Task<IngredientDto> Handle(CreateIngredientForRecipeCommand request, CancellationToken cancellationToken)
        {
            var recipe = await _recipeExistenceChecker.CheckRecipeExistenceAsync(request.RecipeId, cancellationToken);
            var existingIngredient = await _ingredientRepository.GetIngredientByRecipeIdAndNameAsync(request.RecipeId, request.Name, cancellationToken);

            if (existingIngredient != null)
            {
                throw new InvalidOperationException(ErrorMessages.IngredientWithSameNameExists);
            }

            var newIngredient = _mapper.Map<Ingredient>(request);
            existingIngredient = await _ingredientRepository.InsertAsync(newIngredient, cancellationToken);
            await _ingredientRepository.SaveChangesAsync(cancellationToken);
            var recipeIngredient = new RecipeIngredient
            {
                IngredientId = existingIngredient.Id,
                Amount = request.Amount
            };
            recipe.RecipeIngredients.Add(recipeIngredient);
            await _recipeRepository.UpdateAsync(recipe, cancellationToken);

            return _mapper.Map<IngredientDto>(existingIngredient);
        }
    }
}