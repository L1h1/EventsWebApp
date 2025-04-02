using EventsWebApp.Application.DTOs;
using EventsWebApp.Shared.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsWebApp.Application.Queries.User.GetUsersQuery
{
    public sealed record GetUsersQuery(int pageNumber, int pageSize) : IRequest<PaginatedDTO<UserResponseDTO>>;
}
