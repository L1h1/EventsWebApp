using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsWebApp.Application.Commands.User.CreateUserCommand
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(c => c.requestDTO.FirstName).NotEmpty().MaximumLength(64);
            RuleFor(c => c.requestDTO.LastName).NotEmpty().MaximumLength(64);
            RuleFor(c => c.requestDTO.DateOfBirth).NotEmpty();
            RuleFor(c => c.requestDTO.Email).NotEmpty().EmailAddress();
            RuleFor(c => c.requestDTO.Password).NotEmpty();
        }
    }
}
