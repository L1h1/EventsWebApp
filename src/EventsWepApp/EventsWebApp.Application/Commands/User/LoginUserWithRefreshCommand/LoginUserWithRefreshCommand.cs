using EventsWebApp.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsWebApp.Application.Commands.User.LoginUserWithRefreshCommand
{
    public sealed record LoginUserWithRefreshCommand(string refreshToken) : IRequest<LoginResponseDTO>;
}
