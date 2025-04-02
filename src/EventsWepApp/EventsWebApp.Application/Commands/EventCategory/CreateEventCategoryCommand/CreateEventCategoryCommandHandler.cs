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

namespace EventsWebApp.Application.Commands.EventCategory.CreateEventCategoryCommand
{
    public class CreateEventCategoryCommandHandler : IRequestHandler<CreateEventCategoryCommand, EventCategoryResponseDTO>
    {
        private readonly IMapper _mapper;
        private readonly IEventCategoryRepository _eventCategoryRepository;
        private readonly IValidator<CreateEventCategoryCommand> _validator;

        public CreateEventCategoryCommandHandler(
            IMapper mapper,
            IEventCategoryRepository eventCategoryRepository,
            IValidator<CreateEventCategoryCommand> validator)
        {
            _mapper = mapper;
            _eventCategoryRepository = eventCategoryRepository;
            _validator = validator;
        }

        public async Task<EventCategoryResponseDTO> Handle(CreateEventCategoryCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var existingCategory = _eventCategoryRepository.GetByNameAsync(request.name, cancellationToken);
            if (existingCategory != null)
            {
                throw new BadRequestException("Event category with given name already exists.");
            }

            var newCategory = new Domain.Entities.EventCategory
            {
                Name = request.name
            };

            await _eventCategoryRepository.AddAsync(newCategory);

            return _mapper.Map<EventCategoryResponseDTO>(newCategory);
        }
    }
}
