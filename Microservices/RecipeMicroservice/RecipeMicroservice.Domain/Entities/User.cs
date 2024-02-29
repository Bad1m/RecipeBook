using System.Text.Json.Serialization;

namespace RecipeMicroservice.Domain.Entities
{
    public class User : BaseEntity
    {
        public string? UserName { get; set; }
        [JsonIgnore]
        public ICollection<Recipe>? Recipes { get; set; }

        public User(string userName)
        {
            UserName = userName;
        }
    }
}