using AutoMapper;
using MediatR;
using RecipeMicroservice.Application.Dtos;
using RecipeMicroservice.Application.Recipes.Commands.Create;
using RecipeMicroservice.Domain.Entities;
using RecipeMicroservice.Infrastructure.Interfaces;

namespace RecipeMicroservice.Application.Recipes.CommandHandlers.Create
{
    public class CreateRecipeHandler : IRequestHandler<CreateRecipe, RecipeDto>
    {
        private readonly IRecipeRepository _recipeRepository;

        private readonly IMapper _mapper;

        private readonly IIngredientRepository _ingredientRepository;

        public CreateRecipeHandler(IRecipeRepository recipeRepository, IMapper mapper, IIngredientRepository ingredientRepository)
        {
            _recipeRepository = recipeRepository;
            _mapper = mapper;
            _ingredientRepository = ingredientRepository;
        }

        public async Task<RecipeDto> Handle(CreateRecipe request, CancellationToken cancellationToken)
        {
            var recipe = _mapper.Map<Recipe>(request);

            if (request.RecipeIngredients != null)
            {
                foreach (var ingredientRequest in request.RecipeIngredients)
                {
                    var newIngredient = _mapper.Map<Ingredient>(ingredientRequest.Ingredient);
                    await _ingredientRepository.InsertAsync(newIngredient, cancellationToken);
                    await _ingredientRepository.SaveChangesAsync(cancellationToken);
                }
            }

            if (request.Instructions != null)
            {
                var instructions = _mapper.Map<List<Instruction>>(request.Instructions);
                recipe.Instructions = instructions;
            }

            var createdRecipe = await _recipeRepository.InsertAsync(recipe, cancellationToken);
            await _recipeRepository.SaveChangesAsync(cancellationToken);
            var recipeDto = _mapper.Map<RecipeDto>(createdRecipe);

            return recipeDto;
        }
    }
}