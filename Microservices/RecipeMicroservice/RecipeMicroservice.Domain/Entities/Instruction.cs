using System.Text.Json.Serialization;

namespace RecipeMicroservice.Domain.Entities
{
    public class Instruction : BaseEntity
    {
        public int? RecipeId { get; set; }
        public int? StepNumber { get; set; }
        public string? Description { get; set; }
        [JsonIgnore]
        public Recipe? Recipe { get; set; }
    }
}