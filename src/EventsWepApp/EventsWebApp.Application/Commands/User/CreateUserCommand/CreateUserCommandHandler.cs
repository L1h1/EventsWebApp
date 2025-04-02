using AutoMapper;
using EventsWebApp.Application.DTOs;
using EventsWebApp.Domain.Entities;
using EventsWebApp.Domain.Interfaces;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsWebApp.Application.Commands.User.CreateUserCommand
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Unit>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IValidator<CreateUserCommand> _validator;

        public CreateUserCommandHandler(
            IMapper mapper,
            IUserRepository userRepository,
            IValidator<CreateUserCommand> validator)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _validator = validator;
        }

        public async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var newUser = _mapper.Map<Domain.Entities.User>(request.requestDTO);
            newUser.Role = Domain.Enums.UserRole.User;

            await _userRepository.AddAsync(newUser, cancellationToken);
            return Unit.Value;
        }
    }
}
