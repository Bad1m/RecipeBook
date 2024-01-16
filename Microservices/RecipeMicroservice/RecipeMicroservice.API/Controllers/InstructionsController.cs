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
    [Route("api/instructions")]
    public class InstructionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public InstructionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{recipeId}")]
        public async Task<IActionResult> GetInstructionsByRecipeIdAsync([FromRoute] int recipeId, [FromQuery] PaginationSettings paginationSettings, CancellationToken cancellationToken)
        {
            var query = new GetAllInstructionsByRecipeIdQuery { RecipeId = recipeId, PaginationSettings = paginationSettings };
            var result = await _mediator.Send(query, cancellationToken);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateInstructionAsync([FromBody] CreateInstructionForRecipeCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateInstructionAsync([FromBody] UpdateInstructionCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInstructionAsync([FromRoute] int id, CancellationToken cancellationToken)
        {
            var command = new DeleteInstructionCommand { Id = id };
            await _mediator.Send(command, cancellationToken);

            return NoContent();
        }
    }
}
