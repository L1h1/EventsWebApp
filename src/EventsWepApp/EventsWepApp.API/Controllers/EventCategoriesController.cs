using EventsWebApp.Application.Commands.EventCategory.CreateEventCategoryCommand;
using EventsWebApp.Application.Commands.EventCategory.DeleteEventCategoryCommand;
using EventsWebApp.Application.Queries.EventCategory.GetEventCategoriesQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventsWepApp.API.Controllers
{
    [ApiController]
    [Route("api/event-categories")]
    public class EventCategoriesController : Controller
    {
        private readonly IMediator _mediator;

        public EventCategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEventCategories(int pageNumber = 1, int pageSize = 5, CancellationToken cancellationToken = default)
        {
            var query = new GetEventCategoriesQuery(pageNumber, pageSize);
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> CreateEventCategory(string name, CancellationToken cancellationToken = default)
        {
            var command = new CreateEventCategoryCommand(name);
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> DeleteEventCategory(Guid id, CancellationToken cancellationToken = default)
        {
            var command = new DeleteEventCategoryCommand(id);
            await _mediator.Send(command, cancellationToken);
            return Ok(new { message = "Item deletion successful" });
        }
    }
}
