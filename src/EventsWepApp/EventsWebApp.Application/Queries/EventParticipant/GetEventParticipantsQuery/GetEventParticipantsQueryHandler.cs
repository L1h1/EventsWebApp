using AutoMapper;
using EventsWebApp.Application.DTOs;
using EventsWebApp.Domain.Interfaces;
using EventsWebApp.Shared.DTO;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsWebApp.Application.Queries.EventParticipant.GetEventParticipantsQuery
{
    public class GetEventParticipantsQueryHandler : IRequestHandler<GetEventParticipantsQuery, PaginatedDTO<EventParticipantResponseDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IEventParticipantRepository _eventParticipantRepository;

        public GetEventParticipantsQueryHandler(IMapper mapper, IEventParticipantRepository repository)
        {
            _mapper = mapper;
            _eventParticipantRepository = repository;
        }

        public async Task<PaginatedDTO<EventParticipantResponseDTO>> Handle(GetEventParticipantsQuery request, CancellationToken cancellationToken)
        {
            var paginatedData = await _eventParticipantRepository.GetAllASync(
                request.pageNumber,
                request.pageSize,
                query => query
                    .Where(p => p.EventId == request.eventId)
                    .Include(p=>p.User),
                cancellationToken
                );

            var mappedData = _mapper.Map<IEnumerable<EventParticipantResponseDTO>>(paginatedData.Items);

            return new PaginatedDTO<EventParticipantResponseDTO>
            {
                Items = mappedData,
                PageNumber = request.pageNumber,
                PageSize = request.pageSize,
                TotalCount = paginatedData.TotalCount
            };
        }
    }
}
