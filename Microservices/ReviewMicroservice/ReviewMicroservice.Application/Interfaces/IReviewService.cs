using ReviewMicroservice.Application.Dtos;
using ReviewMicroservice.Domain.Models;
using ReviewMicroservice.Domain.Settings;

namespace ReviewMicroservice.Application.Interfaces
{
    public interface IReviewService
    {
        Task<PaginatedResult<ReviewDto>> GetAllAsync(PaginationSettings paginationSettings, CancellationToken cancellationToken);
        Task DeleteByIdAsync(string id, CancellationToken cancellationToken);
        Task<ReviewDto> GetByIdAsync(string id, CancellationToken cancellationToken);
        Task<ReviewDto> InsertAsync(ReviewRequest reviewRequest, CancellationToken cancellationToken);
        Task UpdateAsync(string id, ReviewRequest reviewRequest, CancellationToken cancellationToken);
        Task<PaginatedResult<ReviewDto>> GetByRecipeIdAsync(int recipeId, PaginationSettings paginationSettings, CancellationToken cancellationToken);
    }
}