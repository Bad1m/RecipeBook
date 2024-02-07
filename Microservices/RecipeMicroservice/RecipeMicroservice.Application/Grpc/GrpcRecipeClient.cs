using RecipeMicroservice.Application.Grpc.Protos;
using RecipeMicroservice.Domain.Entities;

namespace RecipeMicroservice.Application.Grpc
{
    public class GrpcRecipeClient
    {
        private readonly GrpcRecipe.GrpcRecipeClient _client;

        public GrpcRecipeClient(GrpcRecipe.GrpcRecipeClient client)
        {
            _client = client;
        }

        public async Task CreateRecipeAsync(Recipe recipe)
        {
            var recipeDto = new RecipeDto { Id = recipe.Id, Dish = recipe.Dish };
            var request = new CreateRecipeRequest 
            { 
                Recipe = recipeDto
            };
            await _client.CreateRecipeAsync(request);
        }

        public async Task UpdateRecipeAsync(int id, Recipe updatedRecipe)
        {
            var recipeDto = new RecipeDto { Dish = updatedRecipe.Dish };
            var request = new UpdateRecipeRequest 
            { 
                Id = id, 
                UpdatedRecipe = recipeDto
            };
            await _client.UpdateRecipeAsync(request);
        }
    }
}