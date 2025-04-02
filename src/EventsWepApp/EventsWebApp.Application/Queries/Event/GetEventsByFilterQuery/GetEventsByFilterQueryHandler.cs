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

namespace EventsWebApp.Application.Queries.Event.GetEventsByFilterQuery
{
    public class GetEventsByFilterQueryHandler : IRequestHandler<GetEventsByFilterQuery, PaginatedDTO<EventResponseDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IEventRepository _eventRepository;
        private readonly IValidator<GetEventsByFilterQuery> _validator;

        public GetEventsByFilterQueryHandler(IMapper mapper, IEventRepository eventRepository, IValidator<GetEventsByFilterQuery> validator)
        {
            _mapper = mapper;
            _validator = validator;
            _eventRepository = eventRepository;
        }

        public async Task<PaginatedDTO<EventResponseDTO>> Handle(GetEventsByFilterQuery request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var rawData = await _eventRepository.GetByFilterAsync(
                request.pageNumber,
                request.pageSize,
                request.filterDTO.EventDateAndTime,
                request.filterDTO.EventAddress,
                request.filterDTO.CategoryId,
                cancellationToken
                );

            if (!rawData.Items.Any())
            {
                throw new NotFoundException("No events found with given parameters.");
            }

            var mappedData = _mapper.Map<IEnumerable<EventResponseDTO>>(rawData.Items);

            return new PaginatedDTO<EventResponseDTO>
            {
                Items = mappedData,
                TotalCount = rawData.TotalCount,
                PageNumber = rawData.PageNumber,
                PageSize = rawData.PageSize,
            };
        }
    }
}
