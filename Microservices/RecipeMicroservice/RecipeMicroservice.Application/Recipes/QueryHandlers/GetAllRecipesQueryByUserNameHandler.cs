using AutoMapper;
using MediatR;
using RecipeMicroservice.Application.Dtos;
using RecipeMicroservice.Application.Recipes.Queries;
using RecipeMicroservice.Domain.Constants;
using RecipeMicroservice.Infrastructure.Interfaces;

namespace RecipeMicroservice.Application.Recipes.QueryHandlers
{
    public class GetAllRecipesQueryByUserNameHandler : IRequestHandler<GetAllRecipesByUserNameQuery, IEnumerable<RecipeDto>>
    {
        private readonly IRecipeRepository _recipeRepository;

        private readonly IMapper _mapper;

        private readonly IUserRepository _userRepository;

        public GetAllRecipesQueryByUserNameHandler(IRecipeRepository recipeRepository, IMapper mapper, IUserRepository userRepository)
        {
            _recipeRepository = recipeRepository;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<RecipeDto>> Handle(GetAllRecipesByUserNameQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByUserNameAsync(request.UserName, cancellationToken);

            if (user == null)
            {
                throw new InvalidOperationException(ErrorMessages.UserNotFound);
            }

            var recipes = await _recipeRepository.GetRecipesByUserNameAsync(user.UserName, cancellationToken);

            return _mapper.Map<IEnumerable<RecipeDto>>(recipes);
        }
    }
}