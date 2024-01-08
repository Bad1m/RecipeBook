using MediatR;
using RecipeMicroservice.Application.Dtos;

namespace RecipeMicroservice.Application.Recipes.Commands.Create
{
    public class CreateRecipe : IRequest<RecipeDto>
    {
        public string? Dish { get; set; }
        public string? Category { get; set; }
        public string? Description { get; set; }
        public TimeSpan PrepTime { get; set; }
        public string? Img { get; set; }
        public ICollection<CreateRecipeIngredient> RecipeIngredients { get; set; }
        public ICollection<CreateInstruction>? Instructions { get; set; }
    }
}