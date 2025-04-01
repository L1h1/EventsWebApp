using AutoMapper;
using EventsWebApp.Application.DTOs;
using EventsWebApp.Domain.Entities;
using EventsWebApp.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsWebApp.Application.Commands.EventParticipant.CreateEventParticipantCommand
{
    public class CreateEventParticipantCommandHandler : IRequestHandler<CreateEventParticipantCommand, EventParticipantResponseDTO>
    {
        private readonly IMapper _mapper;
        private readonly IEventParticipantRepository _eventParticipantRepository;

        public CreateEventParticipantCommandHandler(IMapper mapper, IEventParticipantRepository eventParticipantRepository)
        {
            _mapper = mapper;
            _eventParticipantRepository = eventParticipantRepository;
        }

        public async Task<EventParticipantResponseDTO> Handle(CreateEventParticipantCommand request, CancellationToken cancellationToken)
        {
            var newParticipant = _mapper.Map<Domain.Entities.EventParticipant>(request.requestDTO);
            newParticipant.RegistrationDate = DateOnly.FromDateTime(DateTime.Now);

            await _eventParticipantRepository.AddAsync(newParticipant, cancellationToken);

            return _mapper.Map<EventParticipantResponseDTO>(newParticipant);
        }
    }
}
