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
            var query = new GetAllRecipes { PageNumber = paginationSettings.PageNumber, PageSize = paginationSettings.PageSize };
            var result = await _mediator.Send(query, cancellationToken);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRecipeByIdAsync(int id, CancellationToken cancellationToken)
        {
            var query = new GetRecipeById { Id = id };
            var result = await _mediator.Send(query, cancellationToken);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRecipeAsync([FromBody] CreateRecipe command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRecipeAsync([FromBody] UpdateRecipe command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecipeAsync(int id, CancellationToken cancellationToken)
        {
            var command = new DeleteRecipe { Id = id };
            await _mediator.Send(command, cancellationToken);

            return NoContent();
        }
    }
}