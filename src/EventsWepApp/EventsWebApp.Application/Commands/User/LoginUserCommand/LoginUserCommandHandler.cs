using EventsWebApp.Application.DTOs;
using EventsWebApp.Application.Exceptions;
using EventsWebApp.Domain.Entities;
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
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginResponseDTO>
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly IValidator<LoginUserCommand> _validator;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public LoginUserCommandHandler(
            IUserRepository userRepository,
            IValidator<LoginUserCommand> validator,
            ITokenService tokenService,
            IRefreshTokenRepository refreshTokenRepository)
        {
            _userRepository = userRepository;
            _validator = validator;
            _tokenService = tokenService;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<LoginResponseDTO> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var existingUser = await _userRepository.GetByEmailAsync(request.loginDTO.Email, cancellationToken);

            if (existingUser == null || existingUser.Password != request.loginDTO.Password)
            {
                throw new NotFoundException("User with given credentials does not exist");
            }

            var accessToken = _tokenService.CreateAccessToken(existingUser, cancellationToken);
            var refreshTokenAndExp = _tokenService.CreateRefreshToken();

            var refreshToken = new RefreshToken
            {
                Token = refreshTokenAndExp.Item1,
                UserId = existingUser.Id,
                ExpiresOn = DateTime.Now.AddDays(refreshTokenAndExp.Item2)
            };

            await _refreshTokenRepository.AddAsync(refreshToken, cancellationToken);

            return new LoginResponseDTO
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken.Token
            };
        }
    }
}
