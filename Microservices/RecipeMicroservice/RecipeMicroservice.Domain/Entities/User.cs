namespace RecipeMicroservice.Domain.Entities
{
    public class User : BaseEntity
    {
        public string? UserName { get; set; }
        public ICollection<Recipe>? Recipes { get; set; }

        public User(string userName)
        {
            UserName = userName;
        }
    }
}