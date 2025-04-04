using AutoMapper;
using EventsWebApp.Application.Commands.Event.CreateEventCommand;
using EventsWebApp.Application.DTOs;
using EventsWebApp.Application.Exceptions;
using EventsWebApp.Application.Mapping;
using EventsWebApp.Domain.Interfaces;
using EventsWebApp.Tests.Data;
using FluentValidation;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsWebApp.Tests.Event.Commands
{
    public class CreateEventCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IEventRepository> _eventRepositoryMock;
        private readonly IValidator<CreateEventCommand> _validator;
        private readonly Mock<IEventCategoryRepository> _eventCategoryRepositoryMock;
        private readonly CreateEventCommandHandler _handler;

        public CreateEventCommandHandlerTests()
        {
            _eventCategoryRepositoryMock = new Mock<IEventCategoryRepository>();
            _eventRepositoryMock = new Mock<IEventRepository>();

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new EventProfile());
            });

            _validator = new CreateEventCommandValidator();
            _mapper = mapperConfig.CreateMapper();
            _handler = new CreateEventCommandHandler(_mapper, _eventRepositoryMock.Object, _validator, _eventCategoryRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldThrowValidationException_WhenInvalidDataProvided()
        {
            //Arrange
            var existingCategory = TestData.GetCategories().First();

            var dto = new EventRequestDTO
            {
                Name = "TEST",
                Description = "TEST",
                EventDateAndTime = DateTime.UtcNow,
                EventAddress = "TEST",
                EventCategoryId = existingCategory.Id,
                MaxParticipantCount = -1
            };

            //Act & Assert
            await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(new CreateEventCommand(dto), default));
        }

        [Fact]
        public async Task Handle_ShouldThrowBadRequestException_WhenCreatingEventWithTakenName()
        {
            //Arrange
            var existingEvent = TestData.GetEvents().First();
            var existingCategory = TestData.GetCategories().First();

            var dto = new EventRequestDTO
            {
                Name = existingEvent.Name,
                Description = "TEST",
                EventDateAndTime = DateTime.UtcNow,
                EventAddress = "TEST",
                EventCategoryId = existingCategory.Id,
                MaxParticipantCount = 10
            };

            _eventRepositoryMock.Setup(m => m.GetByNameAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(existingEvent);

            //Act & Assert
            await Assert.ThrowsAsync<BadRequestException>(() => _handler.Handle(new CreateEventCommand(dto), default));
        }

        [Fact]
        public async Task Handle_ShouldThrowNotFoundException_WhenCreatingEventWithInvalidCategoryId()
        {
            //Arrange 
            var dto = new EventRequestDTO
            {
                Name = "HAHHHAHHAHAHAHA",
                Description = "TEST",
                EventDateAndTime = DateTime.UtcNow,
                EventAddress = "TEST",
                EventCategoryId = new Guid("0140b559-6a47-43e5-addf-281a1fff738a"),
                MaxParticipantCount = 10
            };
            _eventRepositoryMock.Setup(m => m.GetByNameAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Domain.Entities.Event)null);

            _eventCategoryRepositoryMock.Setup(m => m.GetByIdAsync(It.IsAny<Guid>(), null, default))
                .ReturnsAsync((Domain.Entities.EventCategory)null);

            //Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(new CreateEventCommand(dto), default));
        }

        [Fact]
        public async Task Handle_ShouldReturnEventResponseDTO_WhenDataIsValid()
        {
            //Arrange
            var existingCategory = TestData.GetCategories().First();

            var dto = new EventRequestDTO
            {
                Name = "HAHHHAHHAHAHAHA",
                Description = "TEST",
                EventDateAndTime = DateTime.UtcNow,
                EventAddress = "TEST",
                EventCategoryId = existingCategory.Id,
                MaxParticipantCount = 10
            };

            _eventRepositoryMock.Setup(m => m.GetByNameAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Domain.Entities.Event)null);

            _eventCategoryRepositoryMock.Setup(m => m.GetByIdAsync(It.IsAny<Guid>(), null, default))
                .ReturnsAsync(existingCategory);

            //Act
            var result = await _handler.Handle(new CreateEventCommand(dto), default);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(dto.Name, result.Name);
            Assert.Equal(dto.Description, result.Description);
            Assert.Equal(dto.EventDateAndTime, result.EventDateAndTime);
            Assert.Equal(dto.EventAddress, result.EventAddress);
            Assert.Equal(existingCategory.Name, result.CategoryName);
            Assert.Equal(dto.MaxParticipantCount, result.MaxParticipantCount);
        }
    }
}
