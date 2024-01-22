using AutoMapper;
using ReviewMicroservice.Application.Interfaces;
using ReviewMicroservice.Domain.Constants;
using ReviewMicroservice.Application.Dtos;
using ReviewMicroservice.Domain.Entities;
using ReviewMicroservice.Infrastructure.Interfaces;
using ReviewMicroservice.Domain.Settings;

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

        public async Task<List<ReviewDto>> GetAllAsync(PaginationSettings paginationSettings, CancellationToken cancellationToken)
        {
            var reviews = await _reviewRepository.GetAllAsync(paginationSettings, cancellationToken);

            return _mapper.Map<List<ReviewDto>>(reviews);
        }

        public async Task DeleteByIdAsync(string id, CancellationToken cancellationToken)
        {
            await CheckExistingReviewAsync(id, cancellationToken);
            await _reviewRepository.DeleteByIdAsync(id, cancellationToken);
        }

        public async Task<ReviewDto> GetByIdAsync(string id, CancellationToken cancellationToken)
        {
            await CheckExistingReviewAsync(id, cancellationToken);
            var review = await _reviewRepository.GetByIdAsync(id, cancellationToken); ;

            return _mapper.Map<ReviewDto>(review);
        }

        public async Task<ReviewDto> InsertAsync(ReviewRequest reviewRequest, CancellationToken cancellationToken)
        {
            var review = _mapper.Map<Review>(reviewRequest);
            await _reviewRepository.InsertAsync(review, cancellationToken);

            return _mapper.Map<ReviewDto>(review);
        }

        public async Task UpdateAsync(string id, ReviewRequest reviewRequest, CancellationToken cancellationToken)
        {
            await CheckExistingReviewAsync(id, cancellationToken);
            var review = await _reviewRepository.GetByIdAsync(id, cancellationToken);
            _mapper.Map(reviewRequest, review);
            await _reviewRepository.UpdateAsync(id, review, cancellationToken);
        }

        public async Task<List<ReviewDto>> GetByRecipeIdAsync(int recipeId, PaginationSettings paginationSettings, CancellationToken cancellationToken)
        {
            var reviews = await _reviewRepository.GetByRecipeIdAsync(recipeId, paginationSettings, cancellationToken);

            return _mapper.Map<List<ReviewDto>>(reviews);
        }

        private async Task CheckExistingReviewAsync(string id, CancellationToken cancellationToken)
        {
            var IsReviewExists = await _reviewRepository.IsReviewExistsAsync(id, cancellationToken);

            if (!IsReviewExists)
            {
                throw new ArgumentNullException(ErrorMessages.ReviewNotFound);
            }
        }
    }
}