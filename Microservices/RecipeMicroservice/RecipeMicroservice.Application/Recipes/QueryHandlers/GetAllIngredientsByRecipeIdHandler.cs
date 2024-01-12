using AutoMapper;
using MediatR;
using RecipeMicroservice.Application.Dtos;
using RecipeMicroservice.Application.Helpers;
using RecipeMicroservice.Application.Recipes.Queries;
using RecipeMicroservice.Domain.Constants;
using RecipeMicroservice.Domain.Settings;
using RecipeMicroservice.Infrastructure.Interfaces;

namespace RecipeMicroservice.Application.Recipes.QueryHandlers
{
    public class GetAllIngredientsByRecipeIdHandler : IRequestHandler<GetAllIngredientsByRecipeIdQuery, IEnumerable<IngredientDto>>
    {
        private readonly IIngredientRepository _ingredientRepository;

        private readonly IMapper _mapper;

        private readonly RecipeExistenceChecker _recipeExistenceChecker;

        public GetAllIngredientsByRecipeIdHandler(IIngredientRepository ingredientRepository, IMapper mapper, RecipeExistenceChecker recipeExistenceChecker)
        {
            _ingredientRepository = ingredientRepository;
            _mapper = mapper;
            _recipeExistenceChecker = recipeExistenceChecker;
        }

        public async Task<IEnumerable<IngredientDto>> Handle(GetAllIngredientsByRecipeIdQuery request, CancellationToken cancellationToken)
        {
            await _recipeExistenceChecker.CheckRecipeExistenceAsync(request.RecipeId, cancellationToken);
            var ingredients = await _ingredientRepository.GetIngredientsByRecipeIdAsync(request.RecipeId, request.PaginationSettings, cancellationToken);
            var ingredientDtos = _mapper.Map<IEnumerable<IngredientDto>>(ingredients);

            return ingredientDtos;
        }
    }
}