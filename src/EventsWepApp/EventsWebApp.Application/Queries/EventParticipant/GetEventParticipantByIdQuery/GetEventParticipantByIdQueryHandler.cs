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

namespace EventsWebApp.Application.Queries.EventParticipant.GetEventParticipantByIdQuery
{
    public class GetEventParticipantByIdQueryHandler : IRequestHandler<GetEventParticipantByIdQuery, EventParticipantResponseDTO>
    {
        private readonly IMapper _mapper;
        private readonly IValidator<GetEventParticipantByIdQuery> _validator;
        private readonly IEventParticipantRepository _eventParticipantRepository;


        public GetEventParticipantByIdQueryHandler(IMapper mapper, IEventParticipantRepository eventParticipantRepository, IValidator<GetEventParticipantByIdQuery> validator)
        {
            _mapper = mapper;
            _eventParticipantRepository = eventParticipantRepository;
            _validator = validator;
        }

        public async Task<EventParticipantResponseDTO> Handle(GetEventParticipantByIdQuery request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var requestedParticipant = await _eventParticipantRepository.GetByIdAsync(
                request.participantId,
                query => query.Include(p => p.User),
                cancellationToken
                );

            return _mapper.Map<EventParticipantResponseDTO>(requestedParticipant);
        }
    }
}
