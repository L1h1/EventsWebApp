﻿using EventsWebApp.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsWebApp.Application.Queries.EventCategory.GetEventCategoriesQuery
{
    public sealed record GetEventCategoriesQuery(int pageNumber, int pageSize) : IRequest<IEnumerable<EventCategoryResponseDTO>>;
}
