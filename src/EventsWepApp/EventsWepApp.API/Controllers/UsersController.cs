using EventsWebApp.Application.Queries.User.GetUserByIdQuery;
using EventsWebApp.Application.Queries.User.GetUsersQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventsWepApp.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : Controller
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> GetAllUsers(int pageNumber = 1, int pageSize = 10, CancellationToken cancellationToken = default)
        {
            var query = new GetUsersQuery(pageNumber, pageSize);
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        [HttpGet("id")]
        [Authorize(Policy = "UserOrAdminPolicy")]
        public async Task<IActionResult> GetUserById(Guid id, CancellationToken cancellationToken = default)
        {
            var query = new GetUserByIdQuery(id);
            var result = await _mediator.Send(query);
            return Ok(result);
        }


    }
}
