using EventsWebApp.Domain.Interfaces;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsWebApp.Application.Commands.Event.DeleteEventCommand
{
    public class DeleteEventCommandHandler : IRequestHandler<DeleteEventCommand, Unit>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IValidator<DeleteEventCommand> _validator;

        public DeleteEventCommandHandler(IEventRepository eventRepository, IValidator<DeleteEventCommand> validator)
        {
            _eventRepository = eventRepository;
            _validator = validator;
        }

        public async Task<Unit> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            await _eventRepository.DeleteAsync(request.id, cancellationToken); 

            return Unit.Value;
        }
    }
}
