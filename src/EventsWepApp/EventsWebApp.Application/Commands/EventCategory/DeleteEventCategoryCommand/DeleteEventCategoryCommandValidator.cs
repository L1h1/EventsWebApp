using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsWebApp.Application.Commands.EventCategory.DeleteEventCategoryCommand
{
    public class DeleteEventCategoryCommandValidator : AbstractValidator<DeleteEventCategoryCommand>
    {
        public DeleteEventCategoryCommandValidator()
        {
            RuleFor(c => c.id).NotEmpty();
        }
    }
}
