using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsWebApp.Application.Commands.EventCategory.CreateEventCategoryCommand
{
    public class CreateEventCategoryCommandValidator : AbstractValidator<CreateEventCategoryCommand>
    {
        public CreateEventCategoryCommandValidator()
        {
            RuleFor(c => c.name).NotEmpty().MaximumLength(64);
        }
    }
}
