using AutoMapper;
using EventsWebApp.Application.DTOs;
using EventsWebApp.Application.Exceptions;
using EventsWebApp.Application.Mapping;
using EventsWebApp.Application.Queries.Event.GetEventsQuery;
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
    public class GetEventsQueryHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IEventRepository> _eventRepositoryMock;
        private readonly IValidator<GetEventsQuery> _validator;
        private readonly GetEventsQueryHandler _handler;

        public GetEventsQueryHandlerTests()
        {
            _eventRepositoryMock = new Mock<IEventRepository>();

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new EventProfile());
            });
            _validator = new GetEventsQueryValidator();
            _mapper = mapperConfig.CreateMapper();
            _handler = new GetEventsQueryHandler(_mapper, _eventRepositoryMock.Object, _validator);

        }

        [Fact]
        public async Task Handle_ShouldThrowValidationException_WhenInvalidDataProvided()
        {
            //Arrange
            int pageNumber = 0;
            int pageSize = 0;

            var query = new GetEventsQuery(pageNumber, pageSize);

            //Act & Assert
            Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(query, default));
        }

        [Fact]
        public async Task Handle_ShouldThrowNotFoundException_WhenNoDataFound()
        {
            int pageNumber = 1;
            int pageSize = 2;

            var query = new GetEventsQuery(pageNumber, pageSize);

            _eventRepositoryMock
                .Setup(m => m.GetAllASync(
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.IsAny<Func<IQueryable<Domain.Entities.Event>, IQueryable<Domain.Entities.Event>>>(),
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
        public async Task Handle_ShouldReturnPaginatedDTO_WhenDataFound()
        {
            int pageNumber = 1;
            int pageSize = 2;

            var query = new GetEventsQuery(pageNumber, pageSize);
            var expectedData = TestData.GetEvents();

            _eventRepositoryMock
                .Setup(m => m.GetAllASync(
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.IsAny<Func<IQueryable<Domain.Entities.Event>, IQueryable<Domain.Entities.Event>>>(),
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
