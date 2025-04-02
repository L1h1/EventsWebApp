using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsWebApp.Application.Queries.EventParticipant.GetEventParticipantByIdQuery
{
    public class GetEventParticipantByIdQueryValidator : AbstractValidator<GetEventParticipantByIdQuery>
    {
        public GetEventParticipantByIdQueryValidator()
        {
            RuleFor(c => c.participantId).NotEmpty();
        }
    }
}
