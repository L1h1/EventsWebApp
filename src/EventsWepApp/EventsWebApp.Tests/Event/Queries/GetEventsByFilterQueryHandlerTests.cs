using AutoMapper;
using EventsWebApp.Application.DTOs;
using EventsWebApp.Application.Exceptions;
using EventsWebApp.Application.Mapping;
using EventsWebApp.Application.Queries.Event.GetEventByIdQuery;
using EventsWebApp.Application.Queries.Event.GetEventsByFilterQuery;
using EventsWebApp.Domain.Interfaces;
using EventsWebApp.Shared.DTO;
using EventsWebApp.Tests.Data;
using FluentValidation;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsWebApp.Tests.Event.Queries
{
    public class GetEventsByFilterQueryHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IEventRepository> _eventRepositoryMock;
        private readonly IValidator<GetEventsByFilterQuery> _validator;
        private readonly GetEventsByFilterQueryHandler _handler;

        public GetEventsByFilterQueryHandlerTests()
        {
            _eventRepositoryMock = new Mock<IEventRepository>();

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new EventProfile());
            });
            _validator = new GetEventsByFilterQueryValidator();
            _mapper = mapperConfig.CreateMapper();
            _handler = new GetEventsByFilterQueryHandler(_mapper, _eventRepositoryMock.Object, _validator);
        }

        [Fact]
        public async Task Handle_ShouldThrowValidationException_WhenInvalidDataProvided()
        {
            //Arrange
            int pageNumber = 0;
            int pageSize = 0;
            var filterDTO = new EventFilterDTO();

            var query = new GetEventsByFilterQuery(pageNumber, pageSize, filterDTO);

            //Act & Assert
            Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(query, default));
        }

        [Fact]
        public async Task Handle_ShouldThrowNotFoundException_WhenNoDataMatchTheFilter()
        {
            int pageNumber = 1;
            int pageSize = 2;
            var filterDTO = new EventFilterDTO
            {
                CategoryId = new Guid("bb75ff8d-cb0f-4fac-8aab-42d4a99eaf17")
            };

            var query = new GetEventsByFilterQuery(pageNumber, pageSize, filterDTO);

            _eventRepositoryMock
                .Setup(m => m.GetByFilterAsync(
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.IsAny<DateTime?>(),
                    It.IsAny<string?>(),
                    It.IsAny<Guid?>(),
                    default))
                .ReturnsAsync(new PaginatedDTO<Domain.Entities.Event>
                {
                    Items = Enumerable.Empty<Domain.Entities.Event>(),
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalCount = 0
                });

            //Act & Assert
            Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(query, default));
        }

        [Fact]
        public async Task Handle_ShouldReturnPaginatedDTO_WhenProvidedDataIsValid()
        {
            int pageNumber = 1;
            int pageSize = 2;
            var filterDTO = new EventFilterDTO
            {
                CategoryId = TestData.GetCategories().First().Id,
            };

            var query = new GetEventsByFilterQuery(pageNumber, pageSize, filterDTO);
            var expectedData = TestData.GetEvents().Where(x => x.EventCategoryId == filterDTO.CategoryId);

            _eventRepositoryMock
                .Setup(m => m.GetByFilterAsync(
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.IsAny<DateTime?>(),
                    It.IsAny<string?>(),
                    It.IsAny<Guid?>(),
                    default))
                .ReturnsAsync(new PaginatedDTO<Domain.Entities.Event>
                {
                    Items = expectedData,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalCount = expectedData.Count()
                });


            //Act
            var result = await _handler.Handle(query, default);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(pageSize, result.PageSize);
            Assert.Equal(pageNumber, result.PageNumber);
            Assert.Equal(expectedData.Count(), result.TotalCount);
        }
    }
}
