using AutoMapper;
using MediatR;
using RecipeMicroservice.Application.Dtos;
using RecipeMicroservice.Application.Recipes.Queries;
using RecipeMicroservice.Domain.Constants;
using RecipeMicroservice.Domain.Entities;
using RecipeMicroservice.Infrastructure.Interfaces;

namespace RecipeMicroservice.Application.Recipes.QueryHandlers
{
    public class GetRecipeByIdHandler : IRequestHandler<GetRecipeById, RecipeDto>
    {
        private readonly IRecipeRepository _recipeRepository;

        private IMapper _mapper;

        public GetRecipeByIdHandler(IRecipeRepository recipeRepository, IMapper mapper)
        {
            _recipeRepository = recipeRepository;
            _mapper = mapper;
        }

        public async Task<RecipeDto> Handle(GetRecipeById request, CancellationToken cancellationToken)
        {
            var existingRecipe = await _recipeRepository.GetByIdAsync(request.Id, cancellationToken);

            if (existingRecipe == null)
            {
                throw new ArgumentNullException(ErrorMessages.RecipeNotFound);
            }

            var recipeDto = _mapper.Map<RecipeDto>(existingRecipe);

            return recipeDto;
        }
    }
}