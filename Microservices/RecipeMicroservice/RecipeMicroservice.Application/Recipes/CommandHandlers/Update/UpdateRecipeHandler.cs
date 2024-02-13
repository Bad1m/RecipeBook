﻿using AutoMapper;
using Hangfire;
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

        private readonly GrpcRecipeClient _recipeClient;
        
        private readonly IBackgroundJobClient _backgroundJobClient;

        public UpdateRecipeHandler(IRecipeRepository recipeRepository, 
            IMapper mapper, 
            IRecipeExistenceChecker recipeExistenceChecker, 
            ICacheRepository cacheRepository,
            GrpcRecipeClient recipeClient)
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
            _backgroundJobClient.Enqueue(() => _cacheRepository.RemoveAsync(CacheKeys.Recipes));
            await _recipeClient.UpdateRecipeAsync(request.Id, recipe);

            return _mapper.Map<RecipeDto>(recipe);
        }
    }
}