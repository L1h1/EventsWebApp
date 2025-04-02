using AutoMapper;
using EventsWebApp.Application.DTOs;
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

        public CreateEventCommandHandler(IMapper mapper, IEventRepository eventRepository, IValidator<CreateEventCommand> validator)
        {
            _mapper = mapper;
            _validator = validator;
            _eventRepository = eventRepository;
        }

        public async Task<EventResponseDTO> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var newEvent = _mapper.Map<Domain.Entities.Event>(request.eventDTO);

            await _eventRepository.AddAsync(newEvent, cancellationToken);

            return _mapper.Map<EventResponseDTO>(newEvent);
        }
    }
}
