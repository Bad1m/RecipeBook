using AutoMapper;
using MediatR;
using RecipeMicroservice.Application.Dtos;
using RecipeMicroservice.Application.Helpers;
using RecipeMicroservice.Application.Recipes.Queries;
using RecipeMicroservice.Domain.Constants;
using RecipeMicroservice.Domain.Entities;
using RecipeMicroservice.Infrastructure.Interfaces;

namespace RecipeMicroservice.Application.Recipes.QueryHandlers
{
    public class GetRecipeByIdHandler : IRequestHandler<GetRecipeByIdQuery, RecipeDto>
    {
        private readonly IRecipeRepository _recipeRepository;

        private IMapper _mapper;

        private readonly RecipeExistenceChecker _recipeExistenceChecker;

        public GetRecipeByIdHandler(IRecipeRepository recipeRepository, IMapper mapper, RecipeExistenceChecker recipeExistenceChecker)
        {
            _recipeRepository = recipeRepository;
            _mapper = mapper;
            _recipeExistenceChecker = recipeExistenceChecker;
        }

        public async Task<RecipeDto> Handle(GetRecipeByIdQuery request, CancellationToken cancellationToken)
        {
            var existingRecipe = await _recipeExistenceChecker.CheckRecipeExistenceAsync(request.Id, cancellationToken);
            var recipeDto = _mapper.Map<RecipeDto>(existingRecipe);

            return recipeDto;
        }
    }
}