using AutoMapper;
using EventsWebApp.Application.DTOs;
using EventsWebApp.Domain.Interfaces;
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

        public UpdateEventCommandHandler(IMapper mapper, IEventRepository eventRepository)
        {
            _mapper = mapper;
            _eventRepository = eventRepository;
        }

        public async Task<EventResponseDTO> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
        {
            var existingEvent = await _eventRepository.GetByIdAsync(request.id, cancellationToken: cancellationToken);

            _mapper.Map(request.requestDTO, existingEvent);
            existingEvent = await _eventRepository.UpdateAsync(existingEvent, cancellationToken);

            return _mapper.Map<EventResponseDTO>(existingEvent);
        }
    }
}
