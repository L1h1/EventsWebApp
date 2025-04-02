using EventsWebApp.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsWebApp.Application.Commands.Event.UploadEventPhotoCommand
{
    public class UploadEventPhotoCommandHandler : IRequestHandler<UploadEventPhotoCommand, Unit>
    {
        private readonly IPhotoService _photoService;

        public UploadEventPhotoCommandHandler(IPhotoService photoService)
        {
            _photoService = photoService;
        }

        public async Task<Unit> Handle(UploadEventPhotoCommand request, CancellationToken cancellationToken)
        {
            await _photoService.UploadPhoto(request.eventId,request.file.OpenReadStream());

            return Unit.Value;
        }
    }
}
