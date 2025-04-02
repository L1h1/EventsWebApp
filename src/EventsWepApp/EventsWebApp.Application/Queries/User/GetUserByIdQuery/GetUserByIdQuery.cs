using EventsWebApp.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsWebApp.Application.Queries.User.GetUserByIdQuery
{
    public sealed record GetUserByIdQuery(Guid id) : IRequest<UserResponseDTO>;
}
