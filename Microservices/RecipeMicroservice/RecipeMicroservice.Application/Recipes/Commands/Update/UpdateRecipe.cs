using MediatR;
using RecipeMicroservice.Application.Dtos;
using RecipeMicroservice.Application.Recipes.Commands.Create;

namespace RecipeMicroservice.Application.Recipes.Commands.Update
{
    public class UpdateRecipe : IRequest<RecipeDto>
    {
        public int Id { get; set; }
        public string? Dish { get; set; }
        public string? Category { get; set; }
        public string? Description { get; set; }
        public TimeSpan PrepTime { get; set; }
        public string? Img { get; set; }
        public List<CreateRecipeIngredient>? UpdatedRecipeIngredients { get; set; }
        public List<CreateInstruction>? UpdatedInstructions { get; set; }
    }
}