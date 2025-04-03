using EventsWebApp.Application.Exceptions;
using EventsWebApp.Domain.Interfaces;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsWebApp.Application.Commands.User.LoginUserCommand
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly IValidator<LoginUserCommand> _validator;

        public LoginUserCommandHandler(IUserRepository userRepository, IValidator<LoginUserCommand> validator, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _validator = validator;
            _tokenService = tokenService;
        }

        public async Task<string> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var existingUser = await _userRepository.GetByEmailAsync(request.loginDTO.Email, cancellationToken);

            if(existingUser == null || existingUser.Password != request.loginDTO.Password)
            {
                throw new NotFoundException("User with given credentials does not exist");
            }

            return await _tokenService.CreateAccessToken(existingUser, cancellationToken);
        }
    }
}
