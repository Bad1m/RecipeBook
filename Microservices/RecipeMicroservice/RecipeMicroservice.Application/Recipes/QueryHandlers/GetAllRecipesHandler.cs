using AutoMapper;
using MediatR;
using RecipeMicroservice.Application.Dtos;
using RecipeMicroservice.Application.Recipes.Queries;
using RecipeMicroservice.Domain.Constants;
using RecipeMicroservice.Domain.Entities;
using RecipeMicroservice.Infrastructure.Interfaces;

namespace RecipeMicroservice.Application.Recipes.QueryHandlers
{
    public class GetAllRecipesHandler : IRequestHandler<GetAllRecipesQuery, IEnumerable<RecipeDto>>
    {
        private readonly IRecipeRepository _recipeRepository;

        private readonly IMapper _mapper;

        private readonly ICacheRepository _cacheRepository;

        public GetAllRecipesHandler(IRecipeRepository recipeRepository, IMapper mapper, ICacheRepository cacheRepository)
        {
            _recipeRepository = recipeRepository;
            _mapper = mapper;
            _cacheRepository = cacheRepository;
        }

        public async Task<IEnumerable<RecipeDto>> Handle(GetAllRecipesQuery request, CancellationToken cancellationToken)
        {
            var recipes = await _cacheRepository.GetDataAsync<IEnumerable<Recipe>>(CacheKeys.Recipes);

            if (recipes == null)
            {
                recipes = await _recipeRepository.GetAllAsync(request.PaginationSettings, cancellationToken);
                await _cacheRepository.SetDataAsync(CacheKeys.Recipes, recipes);
            }

            return _mapper.Map<IEnumerable<RecipeDto>>(recipes);
        }
    }
}