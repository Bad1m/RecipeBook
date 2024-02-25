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

        private readonly ICacheRepository _cacheRepository;

        private readonly IRecipeRepository _recipeRepository;

        private readonly IUserRepository _userRepository;

        public ReviewService(IReviewRepository reviewRepository, 
            IMapper mapper, 
            ICacheRepository cacheRepository, 
            IRecipeRepository recipeRepository, 
            IUserRepository userRepository)
        {
            _reviewRepository = reviewRepository;
            _mapper = mapper;
            _cacheRepository = cacheRepository;
            _recipeRepository = recipeRepository;
            _userRepository = userRepository;
        }

        public async Task<List<ReviewDto>> GetAllAsync(PaginationSettings paginationSettings, CancellationToken cancellationToken)
        {
            var reviews = await _cacheRepository.GetDataAsync<List<Review>>(CacheKeys.Reviews);

            if (reviews == null)
            {
                reviews = await _reviewRepository.GetAllAsync(paginationSettings, cancellationToken);
                await _cacheRepository.SetDataAsync(CacheKeys.Reviews, reviews);
            }

            return _mapper.Map<List<ReviewDto>>(reviews);
        }

        public async Task DeleteByIdAsync(string id, CancellationToken cancellationToken)
        {
            await CheckExistingReviewAsync(id, cancellationToken);
            await _reviewRepository.DeleteByIdAsync(id, cancellationToken);
            await _cacheRepository.RemoveAsync(CacheKeys.Reviews);
        }

        public async Task<ReviewDto> GetByIdAsync(string id, CancellationToken cancellationToken)
        {
            await CheckExistingReviewAsync(id, cancellationToken);
            var reviews = await _cacheRepository.GetDataAsync<List<Review>>(CacheKeys.Reviews);

            if (reviews == null)
            {
                reviews = await _reviewRepository.GetAllAsync(new PaginationSettings(), cancellationToken);
                await _cacheRepository.SetDataAsync(CacheKeys.Reviews, reviews);
            }

            var review = reviews.FirstOrDefault(r => r.Id == id);

            return _mapper.Map<ReviewDto>(review);
        }

        public async Task<ReviewDto> InsertAsync(ReviewRequest reviewRequest, CancellationToken cancellationToken)
        {
            await CheckExistingRecipeAsync(reviewRequest.RecipeId);
            await CheckExistingUserAsync(reviewRequest.UserName, cancellationToken);
            var review = _mapper.Map<Review>(reviewRequest);
            await _reviewRepository.InsertAsync(review, cancellationToken);
            await _cacheRepository.RemoveAsync(CacheKeys.Reviews);

            return _mapper.Map<ReviewDto>(review);
        }

        public async Task UpdateAsync(string id, ReviewRequest reviewRequest, CancellationToken cancellationToken)
        {
            await CheckExistingRecipeAsync(reviewRequest.RecipeId);
            await CheckExistingReviewAsync(id, cancellationToken);
            await CheckExistingUserAsync(reviewRequest.UserName, cancellationToken);
            var review = await _reviewRepository.GetByIdAsync(id, cancellationToken);
            _mapper.Map(reviewRequest, review);
            await _reviewRepository.UpdateAsync(id, review, cancellationToken);
            await _cacheRepository.RemoveAsync(CacheKeys.Reviews);
        }

        public async Task<List<ReviewDto>> GetByRecipeIdAsync(int recipeId, PaginationSettings paginationSettings, CancellationToken cancellationToken)
        {
            var reviews = await _reviewRepository.GetByRecipeIdAsync(recipeId, paginationSettings, cancellationToken);

            return _mapper.Map<List<ReviewDto>>(reviews);
        }

        private async Task CheckExistingRecipeAsync(int recipeId)
        {
            var recipe = await _recipeRepository.GetByIdAsync(recipeId);

            if (recipe == null)
            {
                throw new ArgumentNullException(ErrorMessages.RecipeNotFound);
            }
        }

        private async Task CheckExistingUserAsync(string userName, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByUserNameAsync(userName, cancellationToken);

            if (user == null)
            {
                throw new ArgumentNullException(ErrorMessages.UserNotFound);
            }
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