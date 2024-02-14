namespace RecipeMicroservice.Application.Dtos
{
    public class RecipeIngredientDto 
    {
        public double? Amount { get; set; }
        public IngredientDto? Ingredient { get; set; }
    }
}