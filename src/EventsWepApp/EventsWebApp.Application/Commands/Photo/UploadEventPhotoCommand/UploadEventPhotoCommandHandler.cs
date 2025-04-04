using EventsWebApp.Domain.Interfaces;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsWebApp.Application.Commands.Photo.UploadEventPhotoCommand
{
    public class UploadEventPhotoCommandHandler : IRequestHandler<UploadEventPhotoCommand, Unit>
    {
        private readonly IPhotoService _photoService;
        private readonly IValidator<UploadEventPhotoCommand> _validator;

        public UploadEventPhotoCommandHandler(IPhotoService photoService, IValidator<UploadEventPhotoCommand> validator)
        {
            _validator = validator;
            _photoService = photoService;
        }

        public async Task<Unit> Handle(UploadEventPhotoCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            await _photoService.UploadPhoto(request.eventId, request.file.OpenReadStream());

            return Unit.Value;
        }
    }
}
