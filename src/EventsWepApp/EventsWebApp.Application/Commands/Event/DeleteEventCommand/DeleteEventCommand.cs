using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsWebApp.Application.Commands.Event.DeleteEventCommand
{
    public sealed record DeleteEventCommand(Guid id) : IRequest<Unit>;
}
