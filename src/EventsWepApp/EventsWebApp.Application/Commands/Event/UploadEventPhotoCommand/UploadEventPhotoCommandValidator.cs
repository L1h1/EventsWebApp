using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsWebApp.Application.Commands.Event.UploadEventPhotoCommand
{
    public class UploadEventPhotoCommandValidator : AbstractValidator<UploadEventPhotoCommand>
    {
        public UploadEventPhotoCommandValidator()
        {
            RuleFor(c => c.eventId).NotEmpty();

            RuleFor(c => c.file).NotEmpty();
            RuleFor(c => c.file.ContentType).NotNull().Must(x => x.Equals("image/jpeg") || x.Equals("image/jpg") || x.Equals("image/png"))
                .WithMessage("File type is incorrect.");
        }
    }
}
