using AutoMapper;
using EventsWebApp.Application.DTOs;
using EventsWebApp.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsWebApp.Application.Queries.Event.GetEventsByFilterQuery
{
    public class GetEventsByFilterQueryHandler : IRequestHandler<GetEventsByFilterQuery, IEnumerable<EventResponseDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IEventRepository _eventRepository;

        public GetEventsByFilterQueryHandler(IMapper mapper, IEventRepository eventRepository)
        {
            _mapper = mapper;
            _eventRepository = eventRepository;
        }  

        public async Task<IEnumerable<EventResponseDTO>> Handle(GetEventsByFilterQuery request, CancellationToken cancellationToken)
        {
            var rawData = await _eventRepository.GetByFilterAsync(
                request.eventDateAndTime,
                request.eventAddress,
                request.categoryId,
                cancellationToken
                );

            return _mapper.Map<IEnumerable<EventResponseDTO>>(rawData);
        }
    }
}
