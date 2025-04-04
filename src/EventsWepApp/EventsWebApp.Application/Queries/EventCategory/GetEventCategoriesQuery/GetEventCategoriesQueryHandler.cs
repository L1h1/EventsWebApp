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

namespace EventsWebApp.Application.Queries.EventCategory.GetEventCategoriesQuery
{
    public class GetEventCategoriesQueryHandler : IRequestHandler<GetEventCategoriesQuery, PaginatedDTO<EventCategoryResponseDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IValidator<GetEventCategoriesQuery> _validator;
        private readonly IEventCategoryRepository _eventCategoryRepository;

        public GetEventCategoriesQueryHandler(IMapper mapper, IEventCategoryRepository eventCategoryRepository, IValidator<GetEventCategoriesQuery> validator)
        {
            _mapper = mapper;
            _validator = validator;
            _eventCategoryRepository = eventCategoryRepository;
        }

        public async Task<PaginatedDTO<EventCategoryResponseDTO>> Handle(GetEventCategoriesQuery request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var rawData = await _eventCategoryRepository.GetAllASync(
                request.pageNumber,
                request.pageSize,
                cancellationToken: cancellationToken
                );

            if(!rawData.Items.Any())
            {
                throw new NotFoundException("No event categories found.");
            }

            var mappedData = _mapper.Map<IEnumerable<EventCategoryResponseDTO>>(rawData.Items);

            return new PaginatedDTO<EventCategoryResponseDTO>
            {
                Items = mappedData,
                PageNumber = request.pageNumber,
                PageSize = request.pageSize,
                TotalCount = rawData.TotalCount
            };
        }
    }
}
