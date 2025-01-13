using ReviewMicroservice.Domain.Entities;
using ReviewMicroservice.Domain.Models;
using ReviewMicroservice.Domain.Settings;

namespace ReviewMicroservice.Infrastructure.Interfaces
{
    public interface IReviewRepository
    {
        Task<PaginatedResult<Review>> GetAllAsync(PaginationSettings paginationSettings, CancellationToken cancellationToken);
        Task DeleteByIdAsync(string id, CancellationToken cancellationToken);
        Task<Review> GetByIdAsync(string id, CancellationToken cancellationToken);
        Task InsertAsync(Review review, CancellationToken cancellationToken);
        Task UpdateAsync(string id, Review updatedReview, CancellationToken cancellationToken);
        Task<PaginatedResult<Review>> GetByRecipeIdAsync(int recipeId, PaginationSettings paginationSettings, CancellationToken cancellationToken);
        Task DeleteReviewsByRecipeIdAsync(int recipeId, CancellationToken cancellationToken);
        Task<bool> IsReviewExistsAsync(string id, CancellationToken cancellationToken);
    }
}