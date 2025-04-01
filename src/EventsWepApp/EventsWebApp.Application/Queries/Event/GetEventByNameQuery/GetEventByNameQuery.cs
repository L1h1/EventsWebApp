using EventsWebApp.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsWebApp.Application.Queries.Event.GetEventByNameQuery
{
    public sealed record GetEventByNameQuery(string eventName) : IRequest<EventResponseDTO>;
}
