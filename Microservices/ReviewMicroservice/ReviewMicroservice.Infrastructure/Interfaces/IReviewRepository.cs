using ReviewMicroservice.Domain.Entities;

namespace ReviewMicroservice.Infrastructure.Interfaces
{
    public interface IReviewRepository
    {
        Task<List<Review>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
        Task DeleteByIdAsync(string id, CancellationToken cancellationToken);
        Task<Review> GetByIdAsync(string id, CancellationToken cancellationToken);
        Task InsertAsync(Review review, CancellationToken cancellationToken);
        Task UpdateAsync(string id, Review updatedReview, CancellationToken cancellationToken);
        Task<List<Review>> GetByRecipeIdAsync(string recipeId, int pageNumber, int pageSize, CancellationToken cancellationToken);
        Task<bool> IsReviewExistsAsync(string id, CancellationToken cancellationToken);
    }
}