using EventsWebApp.Application.Commands.Event.CreateEventCommand;
using EventsWebApp.Application.Commands.Event.DeleteEventCommand;
using EventsWebApp.Application.Commands.Event.UpdateEventCommand;
using EventsWebApp.Application.DTOs;
using EventsWebApp.Application.Queries.Event.GetEventByIdQuery;
using EventsWebApp.Application.Queries.Event.GetEventByNameQuery;
using EventsWebApp.Application.Queries.Event.GetEventsByFilterQuery;
using EventsWebApp.Application.Queries.Event.GetEventsQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventsWepApp.API.Controllers
{
    [ApiController]
    [Route("api/events")]
    public class EventsController : Controller
    {
        private readonly IMediator _mediator;

        public EventsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEvents(int pageNumber = 1, int pageSize = 5, CancellationToken cancellationToken = default)
        {
            var query = new GetEventsQuery(pageNumber, pageSize);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEventById(Guid id, CancellationToken cancellationToken = default)
        {
            var query = new GetEventByIdQuery(id);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("by-name/{name}")]
        public async Task<IActionResult> GetEventByName(string name, CancellationToken cancellationToken = default)
        {
            var query = new GetEventByNameQuery(name);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("filtered")]
        public async Task<IActionResult> GetEventsByFilter(int pageNumber, int pageSize, [FromQuery] EventFilterDTO filterDTO, CancellationToken cancellationToken = default)
        {
            var query = new GetEventsByFilterQuery(pageNumber, pageSize, filterDTO);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> CreateEvent([FromBody] EventRequestDTO requestDTO, CancellationToken cancellationToken = default)
        {
            var command = new CreateEventCommand(requestDTO);
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetEventById), new { result.Id }, result);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> UpdateEvent(Guid id, [FromBody] EventRequestDTO requestDTO, CancellationToken cancellationToken = default)
        {
            var command = new UpdateEventCommand(id, requestDTO);
            var result = await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> DeleteEvent(Guid id, CancellationToken cancellationToken = default)
        {
            var command = new DeleteEventCommand(id);
            await _mediator.Send(command);
            return Ok(new { message = "Item deletion successful" });
        }
    }
}
