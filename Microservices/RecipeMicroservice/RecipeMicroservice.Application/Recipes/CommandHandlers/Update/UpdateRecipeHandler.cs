using AutoMapper;
using MediatR;
using RecipeMicroservice.Application.Dtos;
using RecipeMicroservice.Application.Grpc;
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

        private readonly IUserRepository _userRepository;

        private readonly GrpcRecipeClient _recipeClient;

        public UpdateRecipeHandler(IRecipeRepository recipeRepository, 
            IMapper mapper, 
            IRecipeExistenceChecker recipeExistenceChecker, 
            ICacheRepository cacheRepository,
            IUserRepository userRepository,
            GrpcRecipeClient recipeClient)
        {
            _recipeRepository = recipeRepository;
            _mapper = mapper;
            _recipeExistenceChecker = recipeExistenceChecker;
            _cacheRepository = cacheRepository;
            _userRepository = userRepository;
            _recipeClient = recipeClient;
        }

        public async Task<RecipeDto> Handle(UpdateRecipeCommand request, CancellationToken cancellationToken)
        {
            var recipe = await _recipeExistenceChecker.CheckRecipeExistenceAsync(request.Id, cancellationToken);
            var updatedRecipe = _mapper.Map<Recipe>(request);
            var user = await _userRepository.GetByIdAsync((int)recipe.UserId, cancellationToken);
            updatedRecipe.User = user;
            await _recipeRepository.UpdateAsync(updatedRecipe, cancellationToken);
            await _cacheRepository.RemoveAsync(CacheKeys.Recipes);
            await _recipeClient.UpdateRecipeAsync(request.Id, updatedRecipe);

            return _mapper.Map<RecipeDto>(updatedRecipe);
        }
    }
}