namespace RecipeMicroservice.Application.Dtos
{
    public class RecipeDto
    {
        public string Dish { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public TimeSpan PrepTime { get; set; }
        public string Img { get; set; }
        public ICollection<RecipeIngredientDto> RecipeIngredients { get; set; }
        public ICollection<InstructionDto> Instructions { get; set; }
    }
}