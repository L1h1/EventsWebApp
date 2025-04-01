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

namespace EventsWebApp.Application.Commands.EventCategory.CreateEventCategoryCommand
{
    public class CreateEventCategoryCommandHandler : IRequestHandler<CreateEventCategoryCommand, EventCategoryResponseDTO>
    {
        private readonly IMapper _mapper;
        private readonly IEventCategoryRepository _eventCategoryRepository;

        public CreateEventCategoryCommandHandler(IMapper mapper, IEventCategoryRepository eventCategoryRepository)
        {
            _mapper = mapper;
            _eventCategoryRepository = eventCategoryRepository;
        }

        public async Task<EventCategoryResponseDTO> Handle(CreateEventCategoryCommand request, CancellationToken cancellationToken)
        {
            var newCategory = new Domain.Entities.EventCategory
            {
                Name = request.name
            };

            await _eventCategoryRepository.AddAsync(newCategory);

            return _mapper.Map<EventCategoryResponseDTO>(newCategory);
        }
    }
}
