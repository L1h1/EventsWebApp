using AutoMapper;
using EventsWebApp.Application.DTOs;
using EventsWebApp.Application.Exceptions;
using EventsWebApp.Domain.Interfaces;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsWebApp.Application.Commands.Event.CreateEventCommand
{
    public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, EventResponseDTO>
    {
        private readonly IMapper _mapper;
        private readonly IEventRepository _eventRepository;
        private readonly IValidator<CreateEventCommand> _validator;
        private readonly IEventCategoryRepository _eventCategoryRepository;

        public CreateEventCommandHandler(IMapper mapper, IEventRepository eventRepository, IValidator<CreateEventCommand> validator, IEventCategoryRepository eventCategoryRepository)
        {
            _mapper = mapper;
            _validator = validator;
            _eventRepository = eventRepository;
            _eventCategoryRepository = eventCategoryRepository;
        }

        public async Task<EventResponseDTO> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var existingEventWithGivenName = await _eventRepository.GetByNameAsync(request.eventDTO.Name, cancellationToken);
            if(existingEventWithGivenName != null)
            {
                throw new BadRequestException("Event with given name already exists.");
            }

            var existingCategory = await _eventCategoryRepository.GetByIdAsync(request.eventDTO.EventCategoryId, cancellationToken: cancellationToken);
            if (existingCategory == null)
            {
                throw new NotFoundException("Event category does not exist.");
            }

            var newEvent = _mapper.Map<Domain.Entities.Event>(request.eventDTO);
            newEvent.EventCategory = existingCategory;

            await _eventRepository.AddAsync(newEvent, cancellationToken);

            return _mapper.Map<EventResponseDTO>(newEvent);
        }
    }
}
