using AutoMapper;
using MediatR;
using RecipeMicroservice.Application.Dtos;
using RecipeMicroservice.Application.Recipes.Queries;
using RecipeMicroservice.Domain.Constants;
using RecipeMicroservice.Domain.Models;
using RecipeMicroservice.Infrastructure.Interfaces;

namespace RecipeMicroservice.Application.Recipes.QueryHandlers
{
    public class GetAllRecipesHandler : IRequestHandler<GetAllRecipesQuery, PaginatedResult<RecipeDto>>
    {
        private readonly IRecipeRepository _recipeRepository;

        private readonly IMapper _mapper;

        public GetAllRecipesHandler(IRecipeRepository recipeRepository, IMapper mapper)
        {
            _recipeRepository = recipeRepository;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<RecipeDto>> Handle(GetAllRecipesQuery request, CancellationToken cancellationToken)
        {
            var recipes = await _recipeRepository.GetAllAsync(request.PaginationSettings, cancellationToken);

            var paginatedResult = new PaginatedResult<RecipeDto>
            {
                Data = _mapper.Map<IEnumerable<RecipeDto>>(recipes.Data),
                TotalCount = recipes.TotalCount
            };

            return paginatedResult;
        }
    }
}