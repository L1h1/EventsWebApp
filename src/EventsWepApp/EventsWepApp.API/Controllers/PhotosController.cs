using EventsWebApp.Application.Commands.Event.UploadEventPhotoCommand;
using EventsWebApp.Application.Queries.Event.GetEventPhotoQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EventsWepApp.API.Controllers
{
    [ApiController]
    [Route("api/photos")]
    public class PhotosController : Controller
    {
        private readonly IMediator _mediator;

        public PhotosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("events/{id}")]
        [ResponseCache(Duration = 60 * 60 * 24, Location = ResponseCacheLocation.Any)]
        public async Task<IActionResult> GetEventPhoto(Guid id, CancellationToken cancellationToken = default)
        {
            var query = new GetEventPhotoQuery(id);
            var result = await _mediator.Send(query, cancellationToken);
            return File(result, "image/jpeg");
        }

        [HttpPost("events/{id}")]
        public async Task<IActionResult> UploadEventPhoto(Guid id, IFormFile file, CancellationToken cancellationToken = default)
        {
            var command = new UploadEventPhotoCommand(id, file);
            await _mediator.Send(command, cancellationToken);
            return Ok(new { message = "Photo uploaded." });
        }
    }
}
