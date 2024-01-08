using MediatR;
using Microsoft.AspNetCore.Mvc;
using RecipeMicroservice.Application.Recipes.Commands.Create;
using RecipeMicroservice.Application.Recipes.Commands.Delete;
using RecipeMicroservice.Application.Recipes.Commands.Update;
using RecipeMicroservice.Application.Recipes.Queries;
using RecipeMicroservice.Domain.Settings;

namespace RecipeMicroservice.API.Controllers
{
    [ApiController]
    [Route("api/ingredients")]
    public class IngredientsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public IngredientsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllIngredientsByRecipeIdAsync(int id, [FromQuery] PaginationSettings paginationSettings, CancellationToken cancellationToken)
        {
            var query = new GetAllIngredientsByRecipeId { RecipeId = id ,PageNumber = paginationSettings.PageNumber, PageSize = paginationSettings.PageSize };
            var result = await _mediator.Send(query, cancellationToken);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateIngredientAsync([FromBody] CreateIngredientForRecipe command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateIngredientAsync([FromBody] UpdateIngredient command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIngredientAsync(int id, CancellationToken cancellationToken)
        {
            var command = new DeleteIngredient { Id = id };
            await _mediator.Send(command, cancellationToken);

            return NoContent();
        }
    }
}
