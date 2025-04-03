using EventsWebApp.Application.DTOs;
using EventsWebApp.Application.Exceptions;
using EventsWebApp.Domain.Interfaces;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsWebApp.Application.Commands.User.LoginUserWithRefreshCommand
{
    public class LoginUserWithRefreshCommandHandler : IRequestHandler<LoginUserWithRefreshCommand, LoginResponseDTO>
    {
        private readonly ITokenService _tokenService;
        private readonly IUserRepository _userRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IValidator<LoginUserWithRefreshCommand> _validator;

        public LoginUserWithRefreshCommandHandler(
            ITokenService tokenService,
            IUserRepository userRepository,
            IRefreshTokenRepository refreshTokenRepostory,
            IValidator<LoginUserWithRefreshCommand> validator
            )
        {
            _validator = validator;
            _tokenService = tokenService;
            _userRepository = userRepository;
            _refreshTokenRepository = refreshTokenRepostory;
        }
        public async Task<LoginResponseDTO> Handle(LoginUserWithRefreshCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var refreshToken = await _refreshTokenRepository.GetByTokenString(request.refreshToken, cancellationToken);

            if(refreshToken == null)
            {
                throw new NotFoundException("Refresh token does not exist.");
            }

            if(refreshToken.ExpiresOn < DateTime.Now)
            {
                throw new BadRequestException("Provided refresh token expired.");
            }

            var accessToken = _tokenService.CreateAccessToken(refreshToken.User, cancellationToken);
            var newRefreshTokenData = _tokenService.CreateRefreshToken();

            refreshToken.Token = newRefreshTokenData.Item1;
            refreshToken.ExpiresOn = DateTime.Now.AddDays(newRefreshTokenData.Item2);

            await _refreshTokenRepository.UpdateAsync(refreshToken);

            return new LoginResponseDTO
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken.Token
            };
        }
    }
}
