using AutoMapper;
using EventsWebApp.Application.DTOs;
using EventsWebApp.Domain.Interfaces;
using FluentValidation;
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
        private readonly IValidator<GetEventByIdQuery> _validator;

        public GetEventByIdQueryHandler(IMapper mapper, IEventRepository eventRepository, IValidator<GetEventByIdQuery> validator)
        {
            _mapper = mapper;
            _validator = validator;
            _eventRepository = eventRepository;
        }

        public async Task<EventResponseDTO> Handle(GetEventByIdQuery request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var rawData = await _eventRepository.GetByIdAsync(
                request.eventId,
                query => query.Include(e => e.EventCategory),
                cancellationToken
                );

            return _mapper.Map<EventResponseDTO>(rawData);
        }
    }
}
