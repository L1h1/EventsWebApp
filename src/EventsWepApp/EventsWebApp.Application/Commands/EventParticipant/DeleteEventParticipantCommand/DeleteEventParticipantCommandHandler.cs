using EventsWebApp.Domain.Interfaces;
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
        private readonly IEventParticipantRepository _eventParticipantRepository;

        public DeleteEventParticipantCommandHandler(IEventParticipantRepository eventParticipantRepository)
        {
            _eventParticipantRepository = eventParticipantRepository;
        }

        public async Task<Unit> Handle(DeleteEventParticipantCommand request, CancellationToken cancellationToken)
        {
            await _eventParticipantRepository.DeleteAsync(request.id, cancellationToken);

            return Unit.Value;
        }
    }
}
