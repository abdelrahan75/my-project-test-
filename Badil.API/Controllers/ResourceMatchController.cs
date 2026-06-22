using Badil.Application.Features.ResourceMatch.Commands;
using Badil.Application.Features.ResourceMatch.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Badil.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ResourceMatchController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ResourceMatchController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllResourceMatchesQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var result = await _mediator.Send(new GetResourceMatchByIdQuery { Id = id });
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateResourceMatchCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateResourceMatchCommand command)
        {
            try
            {
                command.Id = id;
                await _mediator.Send(command);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _mediator.Send(new DeleteResourceMatchCommand { Id = id });
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpPost("auto-match")]
        public async Task<IActionResult> AutoMatch()
        {
            var count = await _mediator.Send(new AutoMatchCommand());
            return Ok(new { matchesCreated = count });
        }

        [HttpGet("for-listing/{listingId}")]
        public async Task<IActionResult> GetForListing(Guid listingId)
        {
            var result = await _mediator.Send(new GetMatchesForListingQuery { ListingId = listingId });
            return Ok(result);
        }

        [HttpGet("for-request/{requestId}")]
        public async Task<IActionResult> GetForRequest(Guid requestId)
        {
            var result = await _mediator.Send(new GetMatchesForRequestQuery { RequestId = requestId });
            return Ok(result);
        }
    }
}
