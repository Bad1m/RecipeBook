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
    [Route("api/recipes")]
    public class RecipesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RecipesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRecipesAsync([FromQuery] PaginationSettings paginationSettings, CancellationToken cancellationToken)
        {
            var query = new GetAllRecipesQuery { PaginationSettings = paginationSettings };
            var result = await _mediator.Send(query, cancellationToken);

            return Ok(result);
        }

        [HttpGet("userName/{userName}")]
        public async Task<IActionResult> GetAllRecipesByUserNameAsync([FromRoute] string userName, [FromQuery] PaginationSettings paginationSettings, CancellationToken cancellationToken)
        {
            var query = new GetAllRecipesByUserNameQuery { PaginationSettings = paginationSettings, UserName = userName };
            var result = await _mediator.Send(query, cancellationToken);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRecipeByIdAsync([FromRoute] int id, CancellationToken cancellationToken)
        {
            var query = new GetRecipeByIdQuery { Id = id };
            var result = await _mediator.Send(query, cancellationToken);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRecipeAsync([FromBody] CreateRecipeCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRecipeAsync([FromBody] UpdateRecipeCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecipeAsync([FromRoute] int id, CancellationToken cancellationToken)
        {
            var command = new DeleteRecipeCommand { Id = id };
            await _mediator.Send(command, cancellationToken);

            return NoContent();
        }
    }
}