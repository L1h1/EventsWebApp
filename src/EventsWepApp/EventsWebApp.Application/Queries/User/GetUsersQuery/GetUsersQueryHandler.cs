using AutoMapper;
using EventsWebApp.Application.DTOs;
using EventsWebApp.Application.Exceptions;
using EventsWebApp.Domain.Interfaces;
using EventsWebApp.Shared.DTO;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsWebApp.Application.Queries.User.GetUsersQuery
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, PaginatedDTO<UserResponseDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IValidator<GetUsersQuery> _validator;

        public GetUsersQueryHandler(IMapper mapper, IUserRepository userRepository, IValidator<GetUsersQuery> validator)
        {
            _mapper = mapper;
            _validator = validator;
            _userRepository = userRepository;
        }

        public async Task<PaginatedDTO<UserResponseDTO>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var paginatedData = await _userRepository.GetAllASync(
                request.pageNumber,
                request.pageSize,
                cancellationToken: cancellationToken
                );

            if(!paginatedData.Items.Any())
            {
                throw new NotFoundException("No users found");
            }

            var mappedData = _mapper.Map<IEnumerable<UserResponseDTO>>( paginatedData.Items );

            return new PaginatedDTO<UserResponseDTO>
            {
                Items = mappedData,
                PageNumber = paginatedData.PageNumber,
                PageSize = paginatedData.PageSize,
                TotalCount = paginatedData.TotalCount
            };
        }
    }
}
