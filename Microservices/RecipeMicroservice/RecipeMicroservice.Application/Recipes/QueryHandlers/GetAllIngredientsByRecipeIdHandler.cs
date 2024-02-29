using AutoMapper;
using MediatR;
using RecipeMicroservice.Application.Dtos;
using RecipeMicroservice.Application.Interfaces;
using RecipeMicroservice.Application.Recipes.Queries;
using RecipeMicroservice.Infrastructure.Interfaces;

namespace RecipeMicroservice.Application.Recipes.QueryHandlers
{
    public class GetAllIngredientsByRecipeIdHandler : IRequestHandler<GetAllIngredientsByRecipeIdQuery, IEnumerable<IngredientDto>>
    {
        private readonly IIngredientRepository _ingredientRepository;

        private readonly IMapper _mapper;

        private readonly IRecipeExistenceChecker _recipeExistenceChecker;

        public GetAllIngredientsByRecipeIdHandler(IIngredientRepository ingredientRepository, 
            IMapper mapper, 
            IRecipeExistenceChecker recipeExistenceChecker)
        {
            _ingredientRepository = ingredientRepository;
            _mapper = mapper;
            _recipeExistenceChecker = recipeExistenceChecker;
        }

        public async Task<IEnumerable<IngredientDto>> Handle(GetAllIngredientsByRecipeIdQuery request, CancellationToken cancellationToken)
        {
            await _recipeExistenceChecker.CheckRecipeExistenceAsync(request.RecipeId, cancellationToken);
            var ingredients = await _ingredientRepository.GetIngredientsByRecipeIdAsync(request.RecipeId, cancellationToken);
            var ingredientDtos = _mapper.Map<IEnumerable<IngredientDto>>(ingredients);

            return ingredientDtos;
        }
    }
}