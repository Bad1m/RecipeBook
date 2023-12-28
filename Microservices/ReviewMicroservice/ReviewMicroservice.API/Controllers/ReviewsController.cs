using Microsoft.AspNetCore.Mvc;
using ReviewMicroservice.Application.Interfaces;
using ReviewMicroservice.Domain.Dtos;

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
        public async Task<ActionResult<IEnumerable<ReviewDto>>> Get()
        {
            var reviews = await _reviewService.GetAllAsync();

            return Ok(reviews);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReviewDto>> Get(string id)
        {
            var review = await _reviewService.GetByIdAsync(id);

            return Ok(review);
        }

        [HttpGet("recipe/{recipeId}")]
        public async Task<ActionResult<IEnumerable<ReviewDto>>> GetByRecipeId(string recipeId)
        {
            var reviews = await _reviewService.GetByRecipeIdAsync(recipeId);

            return Ok(reviews);
        }

        [HttpPost]
        public async Task<ActionResult<ReviewDto>> Create(ReviewRequest reviewRequest)
        {
            var review = await _reviewService.InsertAsync(reviewRequest);

            return Ok(review);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, ReviewRequest reviewRequest)
        {
            await _reviewService.UpdateAsync(id, reviewRequest);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _reviewService.DeleteByIdAsync(id);

            return NoContent();
        }
    }
}