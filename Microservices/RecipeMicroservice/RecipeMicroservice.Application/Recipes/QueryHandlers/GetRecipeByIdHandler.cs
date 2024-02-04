using AutoMapper;
using MediatR;
using RecipeMicroservice.Application.Dtos;
using RecipeMicroservice.Application.Interfaces;
using RecipeMicroservice.Application.Recipes.Queries;

namespace RecipeMicroservice.Application.Recipes.QueryHandlers
{
    public class GetRecipeByIdHandler : IRequestHandler<GetRecipeByIdQuery, RecipeDto>
    {
        private IMapper _mapper;

        private readonly IRecipeExistenceChecker _recipeExistenceChecker;

        public GetRecipeByIdHandler(IMapper mapper, IRecipeExistenceChecker recipeExistenceChecker)
        {
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