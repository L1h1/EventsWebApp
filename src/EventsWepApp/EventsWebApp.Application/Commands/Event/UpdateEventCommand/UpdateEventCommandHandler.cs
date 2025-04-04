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

namespace EventsWebApp.Application.Commands.Event.UpdateEventCommand
{
    public class UpdateEventCommandHandler : IRequestHandler<UpdateEventCommand, EventResponseDTO>
    {
        private readonly IMapper _mapper;
        private readonly IEventRepository _eventRepository;
        private readonly IValidator<UpdateEventCommand> _validator;
        private readonly IEventCategoryRepository _eventCategoryRepository;

        public UpdateEventCommandHandler(
            IMapper mapper, 
            IEventRepository eventRepository,
            IValidator<UpdateEventCommand> validator,
            IEventCategoryRepository eventCategoryRepository)
        {
            _mapper = mapper;
            _validator = validator;
            _eventRepository = eventRepository;
            _eventCategoryRepository = eventCategoryRepository;
        }

        public async Task<EventResponseDTO> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var existingEvent = await _eventRepository.GetByIdAsync(request.id, cancellationToken: cancellationToken);

            //for cases of changing name to one that's already taken
            var existingEventWithGivenName = await _eventRepository.GetByNameAsync(request.requestDTO.Name, cancellationToken);
            if (existingEventWithGivenName != null && existingEventWithGivenName.Id != existingEvent.Id)
            {
                throw new BadRequestException("Event with given name already exists.");
            }

            var existingCategory = await _eventCategoryRepository.GetByIdAsync(request.requestDTO.EventCategoryId, cancellationToken: cancellationToken);
            if (existingCategory == null)
            {
                throw new NotFoundException("Event category does not exist.");
            }

            _mapper.Map(request.requestDTO, existingEvent);
            existingEvent.EventCategory = existingCategory;
            await _eventRepository.UpdateAsync(existingEvent, cancellationToken);
            

            return _mapper.Map<EventResponseDTO>(existingEvent);
        }
    }
}
