using AutoMapper;
using MediatR;
using RecipeMicroservice.Application.Dtos;
using RecipeMicroservice.Application.Helpers;
using RecipeMicroservice.Application.Recipes.Commands.Update;
using RecipeMicroservice.Domain.Constants;
using RecipeMicroservice.Domain.Entities;
using RecipeMicroservice.Infrastructure.Interfaces;

namespace RecipeMicroservice.Application.Recipes.CommandHandlers.Update
{
    public class UpdateRecipeHandler : IRequestHandler<UpdateRecipeCommand, RecipeDto>
    {
        private readonly IRecipeRepository _recipeRepository;

        private readonly IMapper _mapper;

        private readonly IIngredientRepository _ingredientRepository;

        private readonly RecipeExistenceChecker _recipeExistenceChecker;

        private readonly IInstructionRepository _instructionRepository;

        public UpdateRecipeHandler(IRecipeRepository recipeRepository, IMapper mapper, IIngredientRepository ingredientRepository, RecipeExistenceChecker recipeExistenceChecker, IInstructionRepository instructionRepository)
        {
            _recipeRepository = recipeRepository;
            _mapper = mapper;
            _ingredientRepository = ingredientRepository;
            _recipeExistenceChecker = recipeExistenceChecker;
            _instructionRepository = instructionRepository;
        }

       public async Task<RecipeDto> Handle(UpdateRecipeCommand request, CancellationToken cancellationToken)
        {
            await _recipeExistenceChecker.CheckRecipeExistenceAsync(request.Id, cancellationToken);
            var recipe = _mapper.Map<Recipe>(request);
            await _recipeRepository.UpdateAsync(recipe, cancellationToken);
            recipe = await _recipeRepository.GetByIdAsync(recipe.Id, cancellationToken);
            var updatedRecipeDto = _mapper.Map<RecipeDto>(recipe);

            return updatedRecipeDto;
        }
    }
}