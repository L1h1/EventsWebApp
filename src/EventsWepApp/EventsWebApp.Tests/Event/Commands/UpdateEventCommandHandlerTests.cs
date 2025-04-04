using AutoMapper;
using EventsWebApp.Application.Commands.Event.CreateEventCommand;
using EventsWebApp.Application.Commands.Event.UpdateEventCommand;
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
    public class UpdateEventCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IEventRepository> _eventRepositoryMock;
        private readonly IValidator<UpdateEventCommand> _validator;
        private readonly Mock<IEventCategoryRepository> _eventCategoryRepositoryMock;
        private readonly UpdateEventCommandHandler _handler;

        public UpdateEventCommandHandlerTests()
        {
            _eventCategoryRepositoryMock = new Mock<IEventCategoryRepository>();
            _eventRepositoryMock = new Mock<IEventRepository>();

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new EventProfile());
            });

            _validator = new UpdateEventCommandValidator();
            _mapper = mapperConfig.CreateMapper();
            _handler = new UpdateEventCommandHandler(_mapper, _eventRepositoryMock.Object, _validator, _eventCategoryRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldThrowValidationException_WhenInvalidDataProvided()
        {
            //Arrange
            var existingCategory = TestData.GetCategories().First();
            var existingEvent = TestData.GetEvents().First();

            var dto = new EventRequestDTO
            {
                Name = "TEST",
                Description = "TEST",
                EventDateAndTime = DateTime.UtcNow,
                EventAddress = "TEST",
                EventCategoryId = existingCategory.Id,
                MaxParticipantCount = -1
            };

            var command = new UpdateEventCommand(existingEvent.Id, dto);

            //Act & Assert
            await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, default));
        }

        [Fact]
        public async Task Handle_ShouldThrowBadRequestException_WhenCreatingEventWithTakenName()
        {
            //Arrange
            var existingEvent = TestData.GetEvents().First();
            var duplicateNameEvent = TestData.GetEvents().Last();

            var dto = new EventRequestDTO
            {
                Name = duplicateNameEvent.Name,
                Description = existingEvent.Description,
                EventDateAndTime = existingEvent.EventDateAndTime,
                EventAddress = existingEvent.EventAddress,
                EventCategoryId = existingEvent.EventCategoryId,
                MaxParticipantCount = existingEvent.MaxParticipantCount
            };

            var command = new UpdateEventCommand(existingEvent.Id, dto);

            _eventRepositoryMock.Setup(m => m.GetByIdAsync(It.IsAny<Guid>(), null, default))
                .ReturnsAsync(existingEvent);

            _eventRepositoryMock.Setup(m => m.GetByNameAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(duplicateNameEvent);

            //Act & Assert
            await Assert.ThrowsAsync<BadRequestException>(() => _handler.Handle(command, default));
        }

        [Fact]
        public async Task Handle_ShouldThrowNotFoundException_WhenUpdatingEventWithInvalidCategoryId()
        {
            //Arrange
            var existingEvent = TestData.GetEvents().First();

            var dto = new EventRequestDTO
            {
                Name = existingEvent.Name,
                Description = existingEvent.Description,
                EventDateAndTime = existingEvent.EventDateAndTime,
                EventAddress = existingEvent.EventAddress,
                EventCategoryId = new Guid("0a4144f2-7b5b-4aef-bc99-a8a99d8f7b68"),
                MaxParticipantCount = existingEvent.MaxParticipantCount
            };

            var command = new UpdateEventCommand(existingEvent.Id, dto);

            _eventRepositoryMock.Setup(m => m.GetByIdAsync(It.IsAny<Guid>(), null, default))
                .ReturnsAsync(existingEvent);

            _eventRepositoryMock.Setup(m => m.GetByNameAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(existingEvent);

            _eventCategoryRepositoryMock.Setup(m => m.GetByIdAsync(It.IsAny<Guid>(), null, default))
                .ReturnsAsync((Domain.Entities.EventCategory)null);

            //Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(command, default));
        }

        [Fact]
        public async Task Should_ReturnEventResponseDTO_WhenDataIsValid()
        {
            //Arrange
            var existingEvent = TestData.GetEvents().First();
            var existingCategory = TestData.GetCategories().FirstOrDefault(x => x.Id == existingEvent.EventCategoryId);

            var dto = new EventRequestDTO
            {
                Name = "CHANGED FOR TESTING PURPOSES",
                Description = existingEvent.Description,
                EventDateAndTime = existingEvent.EventDateAndTime,
                EventAddress = existingEvent.EventAddress,
                EventCategoryId = existingEvent.EventCategoryId,
                MaxParticipantCount = existingEvent.MaxParticipantCount
            };

            var command = new UpdateEventCommand(existingEvent.Id, dto);

            _eventRepositoryMock.Setup(m => m.GetByIdAsync(It.IsAny<Guid>(), null, default))
                .ReturnsAsync(existingEvent);

            _eventRepositoryMock.Setup(m => m.GetByNameAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(existingEvent);

            _eventCategoryRepositoryMock.Setup(m => m.GetByIdAsync(It.IsAny<Guid>(), null, default))
                .ReturnsAsync(existingCategory);

            _eventRepositoryMock.Setup(m => m.UpdateAsync(It.IsAny<Domain.Entities.Event>(), default))
                .ReturnsAsync(_mapper.Map<Domain.Entities.Event>(dto));

            //Act
            var result = await _handler.Handle(command, default);

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
