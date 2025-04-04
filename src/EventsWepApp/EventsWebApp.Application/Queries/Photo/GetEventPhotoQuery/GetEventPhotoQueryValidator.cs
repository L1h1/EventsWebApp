using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsWebApp.Application.Queries.Photo.GetEventPhotoQuery
{
    public class GetEventPhotoQueryValidator : AbstractValidator<GetEventPhotoQuery>
    {
        public GetEventPhotoQueryValidator()
        {
            RuleFor(c => c.id).NotEmpty();
        }
    }
}
