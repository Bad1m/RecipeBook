using AutoMapper;
using MediatR;
using RecipeMicroservice.Application.Dtos;
using RecipeMicroservice.Application.Recipes.Queries;
using RecipeMicroservice.Domain.Settings;
using RecipeMicroservice.Infrastructure.Interfaces;

namespace RecipeMicroservice.Application.Recipes.QueryHandlers
{
    public class GetAllIngredientsByRecipeIdHandler : IRequestHandler<GetAllIngredientsByRecipeId, IEnumerable<IngredientDto>>
    {
        private readonly IIngredientRepository _ingredientRepository;

        private readonly IMapper _mapper;

        public GetAllIngredientsByRecipeIdHandler(IIngredientRepository ingredientRepository, IMapper mapper)
        {
            _ingredientRepository = ingredientRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<IngredientDto>> Handle(GetAllIngredientsByRecipeId request, CancellationToken cancellationToken)
        {
            var pagination = new PaginationSettings
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };
            var ingredients = await _ingredientRepository.GetIngredientsByRecipeIdAsync(request.RecipeId, pagination, cancellationToken);
            var ingredientDtos = _mapper.Map<IEnumerable<IngredientDto>>(ingredients);

            return ingredientDtos;
        }
    }
}