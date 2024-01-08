using AutoMapper;
using MediatR;
using RecipeMicroservice.Application.Dtos;
using RecipeMicroservice.Application.Recipes.Commands.Create;
using RecipeMicroservice.Domain.Constants;
using RecipeMicroservice.Domain.Entities;
using RecipeMicroservice.Infrastructure.Interfaces;

namespace RecipeMicroservice.Application.Recipes.CommandHandlers.Create
{
    public class CreateIngredientHandler : IRequestHandler<CreateIngredientForRecipe, IngredientDto>
    {
        private readonly IIngredientRepository _ingredientRepository;

        private readonly IMapper _mapper;

        private readonly IRecipeRepository _recipeRepository;

        public CreateIngredientHandler(IIngredientRepository ingredientRepository, IMapper mapper, IRecipeRepository recipeRepository)
        {
            _ingredientRepository = ingredientRepository;
            _mapper = mapper;
            _recipeRepository = recipeRepository;
        }

        public async Task<IngredientDto> Handle(CreateIngredientForRecipe request, CancellationToken cancellationToken)
        {
            var newIngredient = _mapper.Map<Ingredient>(request);
            var createdIngredient = await _ingredientRepository.InsertAsync(newIngredient, cancellationToken);
            await _ingredientRepository.SaveChangesAsync(cancellationToken);

            if (request.RecipeId != null)
            {
                var recipe = await _recipeRepository.GetByIdAsync(request.RecipeId, cancellationToken);

                if (recipe != null)
                {
                    var recipeIngredient = new RecipeIngredient
                    {
                        IngredientId = createdIngredient.Id,
                        Amount = request.Amount
                    };
                    recipe.RecipeIngredients.Add(recipeIngredient);
                    await _recipeRepository.UpdateAsync(recipe, cancellationToken);
                }

                else
                {
                    throw new InvalidOperationException(ErrorMessages.RecipeNotFound);
                }
            }

            return _mapper.Map<IngredientDto>(createdIngredient);
        }
    }

}