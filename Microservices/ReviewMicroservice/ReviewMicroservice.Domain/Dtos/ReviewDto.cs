namespace ReviewMicroservice.Domain.Dtos
{
    public class ReviewRequest
    {
        public string RecipeId { get; set; }
        public string Comment { get; set; }
        public double Rating { get; set; }
        public DateTime Date { get; set; }
    }
}