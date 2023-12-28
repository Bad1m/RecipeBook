using ReviewMicroservice.Domain.Entities;

namespace ReviewMicroservice.Infrastructure.Interfaces
{
    public interface IReviewRepository
    {
        Task<List<Review>> GetAllAsync();
        Task DeleteByIdAsync(string id);
        Task<Review> GetByIdAsync(string id);
        Task InsertAsync(Review review);
        Task UpdateAsync(string id, Review updatedReview);
        Task<List<Review>> GetByRecipeIdAsync(string recipeId);
    }
}