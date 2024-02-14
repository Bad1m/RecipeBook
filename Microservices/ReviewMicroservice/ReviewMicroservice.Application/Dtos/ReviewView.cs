namespace ReviewMicroservice.Application.Dtos
{
    public class ReviewDto
    {
        public string Id { get; set; }
        public int RecipeId { get; set; }
        public string UserName { get; set; }
        public string Comment { get; set; }
        public double Rating { get; set; }
        public DateTime Date { get; set; }
    }
}