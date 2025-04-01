using AutoMapper;
using EventsWebApp.Application.DTOs;
using EventsWebApp.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsWebApp.Application.Queries.EventCategory.GetEventCategoriesQuery
{
    public class GetEventCategoriesQueryHandler : IRequestHandler<GetEventCategoriesQuery, IEnumerable<EventCategoryResponseDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IEventCategoryRepository _eventCategoryRepository;

        public GetEventCategoriesQueryHandler(IMapper mapper, IEventCategoryRepository eventCategoryRepository)
        {
            _mapper = mapper;
            _eventCategoryRepository = eventCategoryRepository;
        }

        public async Task<IEnumerable<EventCategoryResponseDTO>> Handle(GetEventCategoriesQuery request, CancellationToken cancellationToken)
        {
            var rawData = await _eventCategoryRepository.GetAllASync(
                request.pageNumber,
                request.pageSize,
                cancellationToken: cancellationToken
                );

            return _mapper.Map<IEnumerable<EventCategoryResponseDTO>>(rawData.Items);
        }
    }
}
