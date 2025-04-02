using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsWebApp.Application.Commands.EventParticipant.DeleteEventParticipantCommand
{
    public class DeleteEventParticipantCommandValidator : AbstractValidator<DeleteEventParticipantCommand>
    {
        public DeleteEventParticipantCommandValidator()
        {
            RuleFor(c => c.id).NotEmpty();
        }
    }
}
