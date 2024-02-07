using Grpc.Core;
using ReviewMicroservice.Application.Grpc.Protos;
using ReviewMicroservice.Domain.Constants;
using ReviewMicroservice.Domain.Entities;
using ReviewMicroservice.Infrastructure.Interfaces;

namespace ReviewMicroservice.Application.Grpc
{
    public class GrpcRecipeService : GrpcRecipe.GrpcRecipeBase
    {
        private readonly IRecipeRepository _recipeRepository;

        public GrpcRecipeService(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        public override async Task<RecipeDto> CreateRecipe(CreateRecipeRequest request, ServerCallContext context)
        {
            var recipe = new Recipe
            {
                Dish = request.Recipe.Dish,
                RecipeId = request.Recipe.Id
            };
            await _recipeRepository.InsertAsync(recipe);

            return request.Recipe;
        }

        public override async Task<RecipeDto> UpdateRecipe(UpdateRecipeRequest request, ServerCallContext context)
        {
            var existingRecipe = await _recipeRepository.GetByIdAsync(request.Id);

            if (existingRecipe == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, ErrorMessages.RecipeNotFound));
            }

            existingRecipe.Dish = request.UpdatedRecipe.Dish;
            await _recipeRepository.UpdateAsync(request.UpdatedRecipe.Id, existingRecipe);

            return request.UpdatedRecipe;
        }
    }
}