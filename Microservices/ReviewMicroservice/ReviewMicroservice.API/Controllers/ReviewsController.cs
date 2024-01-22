using Microsoft.AspNetCore.Mvc;
using ReviewMicroservice.Application.Interfaces;
using ReviewMicroservice.Application.Dtos;
using ReviewMicroservice.Domain.Settings;

namespace ReviewMicroservice.API.Controllers
{
    [Route("api/reviews")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewsController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReviewDto>>> GetAsync(CancellationToken cancellationToken, [FromQuery] PaginationSettings paginationSettings)
        {
            var reviews = await _reviewService.GetAllAsync(paginationSettings, cancellationToken);

            return Ok(reviews);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReviewDto>> GetAsync(string id, CancellationToken cancellationToken)
        {
            var review = await _reviewService.GetByIdAsync(id, cancellationToken);

            return Ok(review);
        }

        [HttpGet("recipe/{recipeId}")]
        public async Task<ActionResult<IEnumerable<ReviewDto>>> GetByRecipeIdAsync(int recipeId, CancellationToken cancellationToken, [FromQuery] PaginationSettings paginationSettings)
        {
            var reviews = await _reviewService.GetByRecipeIdAsync(recipeId, paginationSettings, cancellationToken);

            return Ok(reviews);
        }

        [HttpPost]
        public async Task<ActionResult<ReviewDto>> CreateAsync(ReviewRequest reviewRequest, CancellationToken cancellationToken)
        {
            var review = await _reviewService.InsertAsync(reviewRequest, cancellationToken);

            return Ok(review);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(string id, ReviewRequest reviewRequest, CancellationToken cancellationToken)
        {
            await _reviewService.UpdateAsync(id, reviewRequest, cancellationToken);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id, CancellationToken cancellationToken)
        {
            await _reviewService.DeleteByIdAsync(id, cancellationToken);

            return NoContent();
        }
    }
}