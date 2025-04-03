using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsWebApp.Application.Commands.User.LoginUserWithRefreshCommand
{
    public class LoginUserWithRefreshCommandValidator : AbstractValidator<LoginUserWithRefreshCommand>
    {
        public LoginUserWithRefreshCommandValidator()
        {
            RuleFor(x => x.refreshToken).NotEmpty().MaximumLength(200);
        }
    }
}
