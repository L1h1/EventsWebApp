﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsWebApp.Application.Queries.Event.GetEventPhotoQuery
{
    public sealed record GetEventPhotoQuery(Guid id) : IRequest<Stream>;
}
