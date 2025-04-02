using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsWebApp.Application.Queries.EventCategory.GetEventCategoriesQuery
{
    public class GetEventCategoriesQueryValidator : AbstractValidator<GetEventCategoriesQuery>
    {
        public GetEventCategoriesQueryValidator()
        {
            RuleFor(c => c.pageNumber).NotEmpty().GreaterThan(0);
            RuleFor(c => c.pageSize).NotEmpty().GreaterThan(0);
        }
    }
}
