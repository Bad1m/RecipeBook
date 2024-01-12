using AutoMapper;
using MediatR;
using RecipeMicroservice.Application.Dtos;
using RecipeMicroservice.Application.Recipes.Commands.Create;
using RecipeMicroservice.Domain.Constants;
using RecipeMicroservice.Domain.Entities;
using RecipeMicroservice.Infrastructure.Interfaces;

namespace RecipeMicroservice.Application.Recipes.CommandHandlers.Create
{
    public class CreateRecipeHandler : IRequestHandler<CreateRecipeCommand, RecipeDto>
    {
        private readonly IRecipeRepository _recipeRepository;

        private readonly IMapper _mapper;

        public CreateRecipeHandler(IRecipeRepository recipeRepository, IMapper mapper)
        {
            _recipeRepository = recipeRepository;
            _mapper = mapper;
        }

        public async Task<RecipeDto> Handle(CreateRecipeCommand request, CancellationToken cancellationToken)
        {
            var recipe = _mapper.Map<Recipe>(request);
            await _recipeRepository.InsertAsync(recipe, cancellationToken);
            await _recipeRepository.SaveChangesAsync(cancellationToken);
            var createdRecipe = await _recipeRepository.GetByIdAsync(recipe.Id, cancellationToken);
            var recipeDto = _mapper.Map<RecipeDto>(createdRecipe);

            return recipeDto;
        }
    }
}