using AutoMapper;
using EventsWebApp.Application.Exceptions;
using EventsWebApp.Application.Mapping;
using EventsWebApp.Application.Queries.Event.GetEventByIdQuery;
using EventsWebApp.Application.Queries.Event.GetEventByNameQuery;
using EventsWebApp.Domain.Interfaces;
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
    public class GetEventByNameQueryHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IEventRepository> _eventRepositoryMock;
        private readonly IValidator<GetEventByNameQuery> _validator;
        private readonly GetEventByNameQueryHandler _handler;

        public GetEventByNameQueryHandlerTests()
        {
            _eventRepositoryMock = new Mock<IEventRepository>();

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new EventProfile());
            });
            _validator = new GetEventByNameQueryValidator();
            _mapper = mapperConfig.CreateMapper();
            _handler = new GetEventByNameQueryHandler(_mapper, _eventRepositoryMock.Object, _validator);
        }

        [Fact]
        public async Task Handle_ShouldThrowValidationException_WhenInvalidDataProvided()
        {
            //Arrange
            var name = string.Empty;
            var query = new GetEventByNameQuery(name);

            //Act & Assert
            Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(query, default));
        }

        [Fact]
        public async Task Handle_ShouldThrowNotFoundException_WhenInvalidNameProvided()
        {
            //Arrange
            var name = "11111";
            var query = new GetEventByNameQuery(name);

            _eventRepositoryMock.Setup(m => m.GetByNameAsync(It.IsAny<string>(), default))
                .ReturnsAsync((Domain.Entities.Event)null);

            //Act & Assert
            Assert.ThrowsAsync<NotFoundException>(() =>  _handler.Handle(query, default));
        }

        [Fact]
        public async Task Handle_ShouldReturnEventResponseDTO_WhenValidDataProvided()
        {
            //Arrange
            var existingEvent = TestData.GetEvents().First();
            var name = existingEvent.Name;
            var query = new GetEventByNameQuery(name);

            _eventRepositoryMock.Setup(m => m.GetByNameAsync(It.IsAny<string>(), default))
                .ReturnsAsync(existingEvent);

            //Act
            var result = await _handler.Handle(query, default);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(existingEvent.Name, result.Name);
            Assert.Equal(existingEvent.Description, result.Description);
            Assert.Equal(existingEvent.EventDateAndTime, result.EventDateAndTime);
            Assert.Equal(existingEvent.EventAddress, result.EventAddress);
            Assert.Equal(existingEvent.EventCategory.Name, result.CategoryName);
            Assert.Equal(existingEvent.MaxParticipantCount, result.MaxParticipantCount);
        }
    }
}
