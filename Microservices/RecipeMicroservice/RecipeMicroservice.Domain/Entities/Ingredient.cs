using System.Text.Json.Serialization;

namespace RecipeMicroservice.Domain.Entities
{
    public class Ingredient : BaseEntity
    {
        public string? Name { get; set; }
        public string? Unit { get; set; }
        [JsonIgnore]
        public ICollection<RecipeIngredient>? RecipeIngredients { get; set; }
    }
}