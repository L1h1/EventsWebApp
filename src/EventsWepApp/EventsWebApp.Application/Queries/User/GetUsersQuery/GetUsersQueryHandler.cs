using AutoMapper;
using EventsWebApp.Application.DTOs;
using EventsWebApp.Domain.Interfaces;
using EventsWebApp.Shared.DTO;
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

        public GetUsersQueryHandler(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<PaginatedDTO<UserResponseDTO>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var paginatedData = await _userRepository.GetAllASync(
                request.pageNumber,
                request.pageSize,
                cancellationToken: cancellationToken
                );

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
