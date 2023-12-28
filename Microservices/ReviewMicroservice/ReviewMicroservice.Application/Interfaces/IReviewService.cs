using ReviewMicroservice.Domain.Dtos;

namespace ReviewMicroservice.Application.Interfaces
{
    public interface IReviewService
    {
        Task<List<ReviewDto>> GetAllAsync();
        Task DeleteByIdAsync(string id);
        Task<ReviewDto> GetByIdAsync(string id);
        Task<ReviewDto> InsertAsync(ReviewRequest reviewRequest);
        Task UpdateAsync(string id, ReviewRequest reviewRequest);
        Task<List<ReviewDto>> GetByRecipeIdAsync(string recipeId);
    }
}