using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsWebApp.Application.Commands.Event.CreateEventCommand
{
    public class CreateEventCommandValidator : AbstractValidator<CreateEventCommand>
    {
        public CreateEventCommandValidator()
        {
            RuleFor(c => c.eventDTO.Name).NotEmpty().MaximumLength(128);
            RuleFor(c => c.eventDTO.Description).NotEmpty().MaximumLength(512);
            RuleFor(c => c.eventDTO.EventDateAndTime).NotEmpty();
            RuleFor(c => c.eventDTO.EventAddress).NotEmpty().MaximumLength(128);
            RuleFor(c => c.eventDTO.EventCategoryId).NotEmpty();
            RuleFor(c => c.eventDTO.MaxParticipantCount).NotEmpty().GreaterThan(0);
        }
    }
}
