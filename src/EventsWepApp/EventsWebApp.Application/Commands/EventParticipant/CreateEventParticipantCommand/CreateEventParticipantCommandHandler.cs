using AutoMapper;
using EventsWebApp.Application.DTOs;
using EventsWebApp.Domain.Entities;
using EventsWebApp.Domain.Interfaces;
using FluentValidation;
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
        private readonly IValidator<CreateEventParticipantCommand> _validator;
        private readonly IEventParticipantRepository _eventParticipantRepository;

        public CreateEventParticipantCommandHandler(
            IMapper mapper, 
            IEventParticipantRepository eventParticipantRepository,
            IValidator<CreateEventParticipantCommand> validator)
        {
            _mapper = mapper;
            _validator = validator;
            _eventParticipantRepository = eventParticipantRepository;
        }

        public async Task<EventParticipantResponseDTO> Handle(CreateEventParticipantCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var newParticipant = _mapper.Map<Domain.Entities.EventParticipant>(request.requestDTO);
            newParticipant.RegistrationDate = DateOnly.FromDateTime(DateTime.Now);

            await _eventParticipantRepository.AddAsync(newParticipant, cancellationToken);

            return _mapper.Map<EventParticipantResponseDTO>(newParticipant);
        }
    }
}
