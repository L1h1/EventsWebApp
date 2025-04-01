using AutoMapper;
using EventsWebApp.Application.DTOs;
using EventsWebApp.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsWebApp.Application.Queries.Event.GetEventByIdQuery
{
    public class GetEventByIdQueryHandler : IRequestHandler<GetEventByIdQuery, EventResponseDTO>
    {
        private readonly IMapper _mapper;
        private readonly IEventRepository _eventRepository;

        public GetEventByIdQueryHandler(IMapper mapper, IEventRepository eventRepository)
        {
            _mapper = mapper;
            _eventRepository = eventRepository;
        }

        public async Task<EventResponseDTO> Handle(GetEventByIdQuery request, CancellationToken cancellationToken)
        {
            var rawData = await _eventRepository.GetByIdAsync(
                request.eventId,
                query => query.Include(e => e.EventCategory),
                cancellationToken
                );

            return _mapper.Map<EventResponseDTO>(rawData);
        }
    }
}
