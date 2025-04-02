using EventsWebApp.Application.Commands.EventParticipant.CreateEventParticipantCommand;
using EventsWebApp.Application.Commands.EventParticipant.DeleteEventParticipantCommand;
using EventsWebApp.Application.DTOs;
using EventsWebApp.Application.Queries.EventParticipant.GetEventParticipantByIdQuery;
using EventsWebApp.Application.Queries.EventParticipant.GetEventParticipantsQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EventsWepApp.API.Controllers
{
    [ApiController]
    [Route("api/event-participants")]
    public class EventParticipantsController : Controller
    {
        private readonly IMediator _mediator;

        public EventParticipantsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEventParticipantById(Guid id, CancellationToken cancellationToken = default)
        {
            var query = new GetEventParticipantByIdQuery(id);
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        [HttpGet("event/{id}")]
        public async Task<IActionResult> GetEventParticipants(Guid id, int pageNumber = 1, int pageSize = 10, CancellationToken cancellationToken = default)
        {
            var query = new GetEventParticipantsQuery(id, pageNumber, pageSize);
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEventParticipant([FromBody] EventParticipantRequestDTO requestDTO, CancellationToken cancellationToken = default)
        {
            var command = new CreateEventParticipantCommand(requestDTO);
            var result = await _mediator.Send(command, cancellationToken);
            return CreatedAtAction(nameof(GetEventParticipantById), new { result.Id }, result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEventParticipant(Guid id, CancellationToken cancellationToken = default)
        {
            var command = new DeleteEventParticipantCommand(id);
            await _mediator.Send(command, cancellationToken);
            return Ok(new { message = "Item deletion successful" });
        }
    }
}
