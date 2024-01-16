using AutoMapper;
using MediatR;
using RecipeMicroservice.Application.Dtos;
using RecipeMicroservice.Application.Recipes.Queries;
using RecipeMicroservice.Domain.Settings;
using RecipeMicroservice.Infrastructure.Interfaces;

namespace RecipeMicroservice.Application.Recipes.QueryHandlers
{
    public class GetAllRecipesHandler : IRequestHandler<GetAllRecipesQuery, IEnumerable<RecipeDto>>
    {
        private readonly IRecipeRepository _recipeRepository;

        private readonly IMapper _mapper;

        public GetAllRecipesHandler(IRecipeRepository recipeRepository, IMapper mapper)
        {
            _recipeRepository = recipeRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RecipeDto>> Handle(GetAllRecipesQuery request, CancellationToken cancellationToken)
        {
            var recipes = await _recipeRepository.GetAllAsync(request.PaginationSettings, cancellationToken);
            var recipeDtos = _mapper.Map<IEnumerable<RecipeDto>>(recipes);

            return recipeDtos;
        }
    }
}