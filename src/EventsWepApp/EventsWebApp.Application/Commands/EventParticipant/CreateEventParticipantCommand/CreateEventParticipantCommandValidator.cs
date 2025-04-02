using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsWebApp.Application.Commands.EventParticipant.CreateEventParticipantCommand
{
    public class CreateEventParticipantCommandValidator : AbstractValidator<CreateEventParticipantCommand>
    {
        public CreateEventParticipantCommandValidator()
        {
            RuleFor(c => c.requestDTO.UserId).NotEmpty();
            RuleFor(c => c.requestDTO.EventId).NotEmpty();
        }
    }
}
