namespace ReviewMicroservice.Application.Dtos
{
    public class ReviewRequest
    {
        public int RecipeId { get; set; }
        public string Comment { get; set; }
        public double Rating { get; set; }
        public DateTime Date { get; set; }
    }
}