using EventsWebApp.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsWebApp.Application.Queries.Event.GetEventPhotoQuery
{
    internal class GetEventPhotoQueryHandler : IRequestHandler<GetEventPhotoQuery, Stream>
    {
        private readonly IPhotoService _photoService;

        public GetEventPhotoQueryHandler(IPhotoService photoService)
        {
            _photoService = photoService;
        }

        public async Task<Stream> Handle(GetEventPhotoQuery request, CancellationToken cancellationToken)
        {
            return await _photoService.GetPhoto(request.id);
        }
    }
}
