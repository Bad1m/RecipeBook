using AutoMapper;
using MediatR;
using RecipeMicroservice.Application.Dtos;
using RecipeMicroservice.Application.Interfaces;
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

        private readonly IRecipeExistenceChecker _recipeExistenceChecker; 
        
        private readonly ICacheRepository _cacheRepository;

        public UpdateRecipeHandler(IRecipeRepository recipeRepository, IMapper mapper, IRecipeExistenceChecker recipeExistenceChecker, ICacheRepository cacheRepository)
        {
            _recipeRepository = recipeRepository;
            _mapper = mapper;
            _recipeExistenceChecker = recipeExistenceChecker;
            _cacheRepository = cacheRepository;
        }

        public async Task<RecipeDto> Handle(UpdateRecipeCommand request, CancellationToken cancellationToken)
        {
            await _recipeExistenceChecker.CheckRecipeExistenceAsync(request.Id, cancellationToken);
            var recipe = _mapper.Map<Recipe>(request);
            await _recipeRepository.UpdateAsync(recipe, cancellationToken);
            await _cacheRepository.RemoveAsync(CacheKeys.Recipes);

            return _mapper.Map<RecipeDto>(recipe);
        }
    }
}