using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsWebApp.Application.Queries.Event.GetEventByNameQuery
{
    public class GetEventByNameQueryValidator : AbstractValidator<GetEventByNameQuery>
    {
        public GetEventByNameQueryValidator()
        {
            RuleFor(c => c.eventName).NotEmpty().MaximumLength(128);
        }
    }
}
