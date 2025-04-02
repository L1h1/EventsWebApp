using EventsWebApp.Application.DTOs;
using EventsWebApp.Shared.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsWebApp.Application.Queries.Event.GetEventsByFilterQuery
{
    public sealed record GetEventsByFilterQuery(int pageNumber, int pageSize, EventFilterDTO filterDTO) : IRequest<PaginatedDTO<EventResponseDTO>>;
}
