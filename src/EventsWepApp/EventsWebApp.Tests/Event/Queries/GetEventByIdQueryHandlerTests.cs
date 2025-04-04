using AutoMapper;
using EventsWebApp.Application.Exceptions;
using EventsWebApp.Application.Mapping;
using EventsWebApp.Application.Queries.Event.GetEventByIdQuery;
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
    public class GetEventByIdQueryHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IEventRepository> _eventRepositoryMock;
        private readonly IValidator<GetEventByIdQuery> _validator;
        private readonly GetEventByIdQueryHandler _handler;

        public GetEventByIdQueryHandlerTests()
        {
            _eventRepositoryMock = new Mock<IEventRepository>();

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new EventProfile());
            });

            _validator = new GetEventByIdQueryValidator();
            _mapper = mapperConfig.CreateMapper();
            _handler = new GetEventByIdQueryHandler(_mapper, _eventRepositoryMock.Object, _validator);
        }

        [Fact]
        public async Task Handle_ShouldThrowValidationException_WhenInvalidDataProvided()
        {
            //Arrange
            var id = new Guid();
            var query = new GetEventByIdQuery(id);

            //Act & Assert
            Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(query,default));
        }

        [Fact]
        public async Task Handle_ShouldThrowNotFoundException_WhenInvalidIdProvided()
        {
            //Arrange
            var id = new Guid("09fb1075-c471-4904-8a54-7e0912c90b4e");
            var query = new GetEventByIdQuery(id);

            _eventRepositoryMock.Setup(m => m.GetByIdAsync(It.IsAny<Guid>(), null, default))
                .ReturnsAsync((Domain.Entities.Event)null);

            //Act & Assert
            Assert.ThrowsAsync<NotFoundException>(() =>  _handler.Handle(query, default));
        }

        [Fact]
        public async Task Handle_ShouldReturnEventResponseDTO_WhenProvidedIdIsCorrect()
        {
            //Arrange
            var existingEvent = TestData.GetEvents().First();
            var query = new GetEventByIdQuery(existingEvent.Id);

            _eventRepositoryMock
                .Setup(m => m.GetByIdAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<Func<IQueryable<Domain.Entities.Event>, IQueryable<Domain.Entities.Event>>>(),
                    default))
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
