using System.Text.Json.Serialization;

namespace RecipeMicroservice.Domain.Entities
{
    public class RecipeIngredient
    {
        public int? RecipeId { get; set; }
        public int? IngredientId { get; set; }
        public double? Amount { get; set; }
        [JsonIgnore]
        public Recipe? Recipe { get; set; }
        public Ingredient? Ingredient { get; set; }
    }
}