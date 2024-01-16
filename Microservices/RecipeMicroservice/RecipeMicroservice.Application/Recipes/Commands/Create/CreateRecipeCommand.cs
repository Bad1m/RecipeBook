using MediatR;
using RecipeMicroservice.Application.Dtos;

namespace RecipeMicroservice.Application.Recipes.Commands.Create
{
    public class CreateRecipeCommand : IRequest<RecipeDto>
    {
        public string? Dish { get; set; }
        public string? Category { get; set; }
        public string? Description { get; set; }
        public TimeSpan PrepTime { get; set; }
        public string? Img { get; set; }
        public ICollection<CreateRecipeIngredientCommand> RecipeIngredients { get; set; }
        public ICollection<CreateInstructionCommand>? Instructions { get; set; }
    }
}