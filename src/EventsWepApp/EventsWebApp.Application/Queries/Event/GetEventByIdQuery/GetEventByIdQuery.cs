﻿using EventsWebApp.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsWebApp.Application.Queries.Event.GetEventByIdQuery
{
    public sealed record GetEventByIdQuery(Guid eventId) : IRequest<EventResponseDTO>;
}
