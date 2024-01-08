using AutoMapper;
using MediatR;
using RecipeMicroservice.Application.Dtos;
using RecipeMicroservice.Application.Recipes.Commands.Update;
using RecipeMicroservice.Domain.Constants;
using RecipeMicroservice.Domain.Entities;
using RecipeMicroservice.Infrastructure.Interfaces;

namespace RecipeMicroservice.Application.Recipes.CommandHandlers.Update
{
    public class UpdateRecipeHandler : IRequestHandler<UpdateRecipe, RecipeDto>
    {
        private readonly IRecipeRepository _recipeRepository;

        private readonly IMapper _mapper;

        private readonly IIngredientRepository _ingredientRepository;

        public UpdateRecipeHandler(IRecipeRepository recipeRepository, IMapper mapper, IIngredientRepository ingredientRepository)
        {
            _recipeRepository = recipeRepository;
            _mapper = mapper;
            _ingredientRepository = ingredientRepository;
        }

        public async Task<RecipeDto> Handle(UpdateRecipe request, CancellationToken cancellationToken)
        {
            var existingRecipe = await _recipeRepository.GetByIdAsync(request.Id, cancellationToken);

            if (existingRecipe == null)
            {
                throw new ArgumentNullException(ErrorMessages.RecipeNotFound);
            }

            _mapper.Map(request, existingRecipe);

            if (request.UpdatedRecipeIngredients != null)
            {
                foreach (var updatedIngredient in request.UpdatedRecipeIngredients)
                {
                    var newIngredient = _mapper.Map<Ingredient>(updatedIngredient.Ingredient);
                    var createdIngredient = await _ingredientRepository.InsertAsync(newIngredient, cancellationToken);
                    await _ingredientRepository.SaveChangesAsync(cancellationToken);

                    existingRecipe.RecipeIngredients.Add(new RecipeIngredient
                    {
                        IngredientId = createdIngredient.Id,
                        Amount = updatedIngredient.Amount
                    });
                }
            }

            if (request.UpdatedInstructions != null)
            {
                var updatedInstructions = _mapper.Map<List<Instruction>>(request.UpdatedInstructions);
                existingRecipe.Instructions = updatedInstructions;
            }

            await _recipeRepository.UpdateAsync(existingRecipe, cancellationToken);
            var updatedRecipeDto = _mapper.Map<RecipeDto>(existingRecipe);

            return updatedRecipeDto;
        }
    }
}