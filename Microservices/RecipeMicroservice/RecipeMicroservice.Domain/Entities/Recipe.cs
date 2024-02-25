namespace RecipeMicroservice.Domain.Entities
{
    public class Recipe : BaseEntity
    {
        public string? Dish { get; set; }
        public string? Category { get; set; }
        public string? Description { get; set; }
        public TimeSpan PrepTime { get; set; }
        public string? Img { get; set; }
        public int? UserId { get; set; }
        public User? User { get; set; }
        public ICollection<RecipeIngredient>? RecipeIngredients { get; set; }
        public ICollection<Instruction>? Instructions { get; set; }
    }
}