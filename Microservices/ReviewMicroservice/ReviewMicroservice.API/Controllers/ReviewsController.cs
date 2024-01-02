using Microsoft.AspNetCore.Mvc;
using ReviewMicroservice.Application.Interfaces;
using ReviewMicroservice.Application.Dtos;

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
        public async Task<ActionResult<IEnumerable<ReviewDto>>> Get(CancellationToken cancellationToken, int page = 1, int pageSize = 10)
        {
            var reviews = await _reviewService.GetAllAsync(page, pageSize, cancellationToken);

            return Ok(reviews);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReviewDto>> Get(string id, CancellationToken cancellationToken)
        {
            var review = await _reviewService.GetByIdAsync(id, cancellationToken);

            return Ok(review);
        }

        [HttpGet("recipe/{recipeId}")]
        public async Task<ActionResult<IEnumerable<ReviewDto>>> GetByRecipeId(string recipeId, CancellationToken cancellationToken, int page = 1, int pageSize = 10)
        {
            var reviews = await _reviewService.GetByRecipeIdAsync(recipeId, page, pageSize, cancellationToken);

            return Ok(reviews);
        }

        [HttpPost]
        public async Task<ActionResult<ReviewDto>> Create(ReviewRequest reviewRequest, CancellationToken cancellationToken)
        {
            var review = await _reviewService.InsertAsync(reviewRequest, cancellationToken);

            return Ok(review);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, ReviewRequest reviewRequest, CancellationToken cancellationToken)
        {
            await _reviewService.UpdateAsync(id, reviewRequest, cancellationToken);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id, CancellationToken cancellationToken)
        {
            await _reviewService.DeleteByIdAsync(id, cancellationToken);

            return NoContent();
        }
    }
}