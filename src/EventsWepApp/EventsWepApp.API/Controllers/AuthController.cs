using EventsWebApp.Application.Commands.User.CreateUserCommand;
using EventsWebApp.Application.Commands.User.LoginUserCommand;
using EventsWebApp.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Mvc;

namespace EventsWepApp.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : Controller
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> CreateUser([FromBody] UserRequestDTO requestDTO, CancellationToken cancellationToken = default)
        {
            var command = new CreateUserCommand(requestDTO);
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(new { message = "Registration successful" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] UserLoginDTO loginDTO, CancellationToken cancellationToken = default)
        {
            var command = new LoginUserCommand(loginDTO);
            var result = await _mediator.Send(command, cancellationToken);
            return Ok( new { AccessToken = result });
        }
    }
}
