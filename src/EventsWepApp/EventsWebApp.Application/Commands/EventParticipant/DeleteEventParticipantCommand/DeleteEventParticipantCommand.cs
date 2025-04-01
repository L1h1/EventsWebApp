using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsWebApp.Application.Commands.EventParticipant.DeleteEventParticipantCommand
{
    public sealed record DeleteEventParticipantCommand(Guid id) : IRequest<Unit>;
}
