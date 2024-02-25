using AutoMapper;
using MediatR;
using RecipeMicroservice.Application.Dtos;
using RecipeMicroservice.Application.Grpc;
using RecipeMicroservice.Application.Recipes.Commands.Create;
using RecipeMicroservice.Domain.Constants;
using RecipeMicroservice.Domain.Entities;
using RecipeMicroservice.Infrastructure.Interfaces;

namespace RecipeMicroservice.Application.Recipes.CommandHandlers.Create
{
    public class CreateRecipeHandler : IRequestHandler<CreateRecipeCommand, RecipeDto>
    {
        private readonly IRecipeRepository _recipeRepository;

        private readonly IMapper _mapper;

        private readonly ICacheRepository _cacheRepository;

        private readonly IUserRepository _userRepository;

        private readonly GrpcRecipeClient _recipeClient;

        public CreateRecipeHandler(IRecipeRepository recipeRepository, 
            IMapper mapper, 
            ICacheRepository cacheRepository, 
            IUserRepository userRepository,
            GrpcRecipeClient recipeClient)
        {
            _recipeRepository = recipeRepository;
            _mapper = mapper;
            _cacheRepository = cacheRepository;
            _userRepository = userRepository;
            _recipeClient = recipeClient;
        }

        public async Task<RecipeDto> Handle(CreateRecipeCommand request, CancellationToken cancellationToken)
        {
            var recipe = _mapper.Map<Recipe>(request);
            var user = await _userRepository.GetByUserNameAsync(request.UserName, cancellationToken);

            if (user == null)
            {
                throw new InvalidOperationException(ErrorMessages.UserNotFound);
            }

            recipe.UserId = user.Id;
            var createdRecipe = await _recipeRepository.InsertAsync(recipe, cancellationToken);
            await _recipeRepository.SaveChangesAsync(cancellationToken);
            await _cacheRepository.RemoveAsync(CacheKeys.Recipes);
            var recipeDto = _mapper.Map<RecipeDto>(createdRecipe);
            recipeDto.User = new UserDto(request.UserName);
            await _recipeClient.CreateRecipeAsync(createdRecipe);

            return recipeDto;
        }
    }
}