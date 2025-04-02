using EventsWebApp.Domain.Interfaces;
using FluentValidation;
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
        private readonly IValidator<GetEventPhotoQuery> _validator;

        public GetEventPhotoQueryHandler(IPhotoService photoService, IValidator<GetEventPhotoQuery> validator)
        {
            _validator = validator;
            _photoService = photoService;
        }

        public async Task<Stream> Handle(GetEventPhotoQuery request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            return await _photoService.GetPhoto(request.id);
        }
    }
}
