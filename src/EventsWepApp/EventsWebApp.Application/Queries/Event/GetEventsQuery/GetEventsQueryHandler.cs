using AutoMapper;
using EventsWebApp.Application.DTOs;
using EventsWebApp.Application.Exceptions;
using EventsWebApp.Domain.Interfaces;
using EventsWebApp.Shared.DTO;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsWebApp.Application.Queries.Event.GetEventsQuery
{
    public class GetEventsQueryHandler : IRequestHandler<GetEventsQuery, PaginatedDTO<EventResponseDTO>>
    {
        private readonly IMapper _mapper;
        private IValidator<GetEventsQuery> _validator;
        private readonly IEventRepository _eventRepository;

        public GetEventsQueryHandler(IMapper mapper, IEventRepository eventRepository, IValidator<GetEventsQuery> validator)
        {
            _mapper = mapper;
            _validator = validator;
            _eventRepository = eventRepository;
        }

        public async Task<PaginatedDTO<EventResponseDTO>> Handle(GetEventsQuery request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var paginatedData = await _eventRepository.GetAllASync(
                request.pageNumber,
                request.pageSize,
                query=>query.Include(e=>e.EventCategory),
                cancellationToken
                );

            if(!paginatedData.Items.Any())
            {
                throw new NotFoundException("No events found");
            }

            var mappedData = _mapper.Map<IEnumerable<EventResponseDTO>>(paginatedData.Items);

            return new PaginatedDTO<EventResponseDTO> 
            { 
                Items = mappedData,
                PageNumber = request.pageNumber,
                PageSize = request.pageSize,
                TotalCount = paginatedData.TotalCount
            };
        }
    }
}
