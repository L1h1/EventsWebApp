using EventsWebApp.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsWebApp.Application.Queries.Event.GetEventsByFilterQuery
{
    public sealed record GetEventsByFilterQuery(
        DateTime? eventDateAndTime = null,
        string eventAddress = null,
        Guid? categoryId = null) : IRequest<IEnumerable<EventResponseDTO>>;
}
