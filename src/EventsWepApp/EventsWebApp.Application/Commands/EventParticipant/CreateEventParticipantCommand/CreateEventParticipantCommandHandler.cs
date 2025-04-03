using AutoMapper;
using EventsWebApp.Application.DTOs;
using EventsWebApp.Application.Exceptions;
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
        private readonly IUserRepository _userRepository;
        private readonly IEventRepository _eventRepository;
        private readonly IEventParticipantRepository _eventParticipantRepository;

        public CreateEventParticipantCommandHandler(
            IMapper mapper,
            IEventParticipantRepository eventParticipantRepository,
            IValidator<CreateEventParticipantCommand> validator,
            IEventRepository eventRepository,
            IUserRepository userRepository)
        {
            _mapper = mapper;
            _validator = validator;
            _eventParticipantRepository = eventParticipantRepository;
            _userRepository = userRepository;
            _eventRepository = eventRepository;
        }

        public async Task<EventParticipantResponseDTO> Handle(CreateEventParticipantCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var existingEvent = await _eventRepository.GetByIdAsync(request.requestDTO.EventId, cancellationToken: cancellationToken);
            if (existingEvent == null)
            {
                throw new NotFoundException("Event with given Id does not exist.");
            }

            var exstingUser = await _userRepository.GetByIdAsync(request.requestDTO.UserId, cancellationToken: cancellationToken);
            if(exstingUser == null)
            {
                throw new NotFoundException("User with given Id does not exist.");
            }

            var alreadyParticipates = await _eventParticipantRepository.CheckParticipation(request.requestDTO.UserId, request.requestDTO.EventId, cancellationToken);
            if (alreadyParticipates)
            {
                throw new BadRequestException("This user already participates in given event.");
            }

            var newParticipant = _mapper.Map<Domain.Entities.EventParticipant>(request.requestDTO);
            newParticipant.RegistrationDate = DateOnly.FromDateTime(DateTime.Now);

            await _eventParticipantRepository.AddAsync(newParticipant, cancellationToken);

            return _mapper.Map<EventParticipantResponseDTO>(newParticipant);
        }
    }
}
