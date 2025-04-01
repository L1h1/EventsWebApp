using AutoMapper;
using EventsWebApp.Application.DTOs;
using EventsWebApp.Domain.Interfaces;
using EventsWebApp.Shared.DTO;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsWebApp.Application.Queries.Event.GetEventsQuery
{
    public class GetEventsQueryHandler : IRequestHandler<GetEventsQuery, PaginatedDTO<EventResponseDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IEventRepository _eventRepository;

        public GetEventsQueryHandler(IMapper mapper, IEventRepository eventRepository)
        {
            _mapper = mapper;
            _eventRepository = eventRepository;
        }

        public async Task<PaginatedDTO<EventResponseDTO>> Handle(GetEventsQuery request, CancellationToken cancellationToken)
        {
            var paginatedData = await _eventRepository.GetAllASync(
                request.pageNumber,
                request.pageSize,
                query=>query.Include(e=>e.EventCategory),
                cancellationToken
                );

            var mappedData = _mapper.Map<IEnumerable<EventResponseDTO>>(paginatedData.Items);

            return new PaginatedDTO<EventResponseDTO> 
            { 
                Items = mappedData,
                PageNumber = request.pageNumber,
                PageSize = request.pageSize,
                TotalCount = paginatedData.TotalCount
            };
        }
    }
}
