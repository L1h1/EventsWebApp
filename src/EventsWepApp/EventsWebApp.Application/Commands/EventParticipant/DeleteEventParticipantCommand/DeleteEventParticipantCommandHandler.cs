using EventsWebApp.Application.Exceptions;
using EventsWebApp.Domain.Interfaces;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsWebApp.Application.Commands.EventParticipant.DeleteEventParticipantCommand
{
    public class DeleteEventParticipantCommandHandler : IRequestHandler<DeleteEventParticipantCommand, Unit>
    {
        private readonly IValidator<DeleteEventParticipantCommand> _validator;
        private readonly IEventParticipantRepository _eventParticipantRepository;

        public DeleteEventParticipantCommandHandler(
            IEventParticipantRepository eventParticipantRepository,
            IValidator<DeleteEventParticipantCommand> validator)
        {
            _validator = validator;
            _eventParticipantRepository = eventParticipantRepository;
        }

        public async Task<Unit> Handle(DeleteEventParticipantCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var existingEventParticipant = await _eventParticipantRepository.GetByIdAsync(request.id, cancellationToken: cancellationToken);
            if (existingEventParticipant == null)
            {
                throw new NotFoundException("Event participant with given Id does not exist");
            }

            await _eventParticipantRepository.DeleteAsync(request.id, cancellationToken);

            return Unit.Value;
        }
    }
}
