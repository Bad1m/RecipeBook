using AutoMapper;
using ReviewMicroservice.Application.Interfaces;
using ReviewMicroservice.Domain.Dtos;
using ReviewMicroservice.Domain.Entities;
using ReviewMicroservice.Infrastructure.Interfaces;

namespace ReviewMicroservice.Application.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;

        public ReviewService(IReviewRepository reviewRepository, IMapper mapper)
        {
            _reviewRepository = reviewRepository;
            _mapper = mapper;
        }

        public async Task<List<ReviewDto>> GetAllAsync()
        {
            var reviews = await _reviewRepository.GetAllAsync();

            return _mapper.Map<List<ReviewDto>>(reviews);
        }

        public async Task DeleteByIdAsync(string id)
        {
            await _reviewRepository.DeleteByIdAsync(id);
        }

        public async Task<ReviewDto> GetByIdAsync(string id)
        {
            var review = await _reviewRepository.GetByIdAsync(id);

            return _mapper.Map<ReviewDto>(review);
        }

        public async Task<ReviewDto> InsertAsync(ReviewRequest reviewRequest)
        {
            var review = _mapper.Map<Review>(reviewRequest);
            await _reviewRepository.InsertAsync(review);

            return _mapper.Map<ReviewDto>(review);
        }

        public async Task UpdateAsync(string id, ReviewRequest reviewRequest)
        {
            var review = await _reviewRepository.GetByIdAsync(id);
            _mapper.Map(reviewRequest, review);
            await _reviewRepository.UpdateAsync(id, review);
        }

        public async Task<List<ReviewDto>> GetByRecipeIdAsync(string recipeId)
        {
            var reviews = await _reviewRepository.GetByRecipeIdAsync(recipeId);

            return _mapper.Map<List<ReviewDto>>(reviews);
        }
    }
}