using EventsWebApp.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsWebApp.Application.Commands.EventParticipant.CreateEventParticipantCommand
{
    public sealed record CreateEventParticipantCommand(EventParticipantRequestDTO requestDTO) : IRequest<EventParticipantResponseDTO>;
}
