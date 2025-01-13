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
        public async Task<IActionResult> GetAllIngredientsByRecipeIdAsync([FromRoute] int id, CancellationToken cancellationToken)
        {
            var query = new GetAllIngredientsByRecipeIdQuery { RecipeId = id };
            var result = await _mediator.Send(query, cancellationToken);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateIngredientAsync([FromBody] CreateIngredientForRecipeCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateIngredientAsync([FromBody] UpdateIngredientCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIngredientAsync([FromRoute] int id, CancellationToken cancellationToken)
        {
            var command = new DeleteIngredientCommand { Id = id };
            await _mediator.Send(command, cancellationToken);

            return NoContent();
        }
    }
}
