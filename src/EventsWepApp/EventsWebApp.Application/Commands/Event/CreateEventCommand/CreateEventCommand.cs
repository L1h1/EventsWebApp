using EventsWebApp.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsWebApp.Application.Commands.Event.CreateEventCommand
{
    public sealed record CreateEventCommand(EventRequestDTO eventDTO) : IRequest<EventResponseDTO>;
}
