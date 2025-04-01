using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsWebApp.Application.Commands.EventCategory.DeleteEventCategoryCommand
{
    public sealed record DeleteEventCategoryCommand(Guid id) : IRequest<Unit>;
}
