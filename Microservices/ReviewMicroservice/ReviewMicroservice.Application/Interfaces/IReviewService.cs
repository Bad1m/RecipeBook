using ReviewMicroservice.Application.Dtos;

namespace ReviewMicroservice.Application.Interfaces
{
    public interface IReviewService
    {
        Task<List<ReviewDto>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
        Task DeleteByIdAsync(string id, CancellationToken cancellationToken);
        Task<ReviewDto> GetByIdAsync(string id, CancellationToken cancellationToken);
        Task<ReviewDto> InsertAsync(ReviewRequest reviewRequest, CancellationToken cancellationToken);
        Task UpdateAsync(string id, ReviewRequest reviewRequest, CancellationToken cancellationToken);
        Task<List<ReviewDto>> GetByRecipeIdAsync(string recipeId, int pageNumber, int pageSize, CancellationToken cancellationToken);
    }
}