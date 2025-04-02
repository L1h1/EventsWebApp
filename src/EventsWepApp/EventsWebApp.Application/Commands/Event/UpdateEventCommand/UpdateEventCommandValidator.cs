using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsWebApp.Application.Commands.Event.UpdateEventCommand
{
    public class UpdateEventCommandValidator : AbstractValidator<UpdateEventCommand>
    {
        public UpdateEventCommandValidator()
        {
            RuleFor(c => c.id).NotEmpty();
            RuleFor(c => c.requestDTO.Name).NotEmpty().MaximumLength(128);
            RuleFor(c => c.requestDTO.Description).NotEmpty().MaximumLength(512);
            RuleFor(c => c.requestDTO.EventDateAndTime).NotEmpty();
            RuleFor(c => c.requestDTO.EventAddress).NotEmpty().MaximumLength(128);
            RuleFor(c => c.requestDTO.EventCategoryId).NotEmpty();
            RuleFor(c => c.requestDTO.MaxParticipantCount).NotEmpty().GreaterThan(0);
        }
    }
}
