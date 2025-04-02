using EventsWebApp.Application.Exceptions;
using EventsWebApp.Domain.Interfaces;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsWebApp.Application.Commands.EventCategory.DeleteEventCategoryCommand
{
    public class DeleteEventCategoryCommandHandler : IRequestHandler<DeleteEventCategoryCommand, Unit>
    {
        private readonly IEventCategoryRepository _eventCategoryRepository;
        private readonly IValidator<DeleteEventCategoryCommand> _validator;

        public DeleteEventCategoryCommandHandler(
            IEventCategoryRepository eventCategoryRepository,
            IValidator<DeleteEventCategoryCommand> validator)
        {
            _eventCategoryRepository = eventCategoryRepository;
            _validator = validator;
        }

        public async Task<Unit> Handle(DeleteEventCategoryCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var existinigCategory = await _eventCategoryRepository.GetByIdAsync(request.id, cancellationToken: cancellationToken);
            if (existinigCategory == null)
            {
                throw new NotFoundException("Event category with given Id does not exist");
            }

            await _eventCategoryRepository.DeleteAsync(request.id);

            return Unit.Value;
        }
    }
}
