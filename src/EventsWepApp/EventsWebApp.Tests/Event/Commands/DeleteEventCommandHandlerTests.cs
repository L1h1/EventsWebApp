using EventsWebApp.Application.Commands.Event.CreateEventCommand;
using EventsWebApp.Application.Commands.Event.DeleteEventCommand;
using EventsWebApp.Application.Exceptions;
using EventsWebApp.Domain.Interfaces;
using EventsWebApp.Tests.Data;
using FluentValidation;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsWebApp.Tests.Event.Commands
{
    public class DeleteEventCommandHandlerTests
    {
        private readonly Mock<IEventRepository> _eventRepositoryMock;
        private readonly IValidator<DeleteEventCommand> _validator;
        private readonly DeleteEventCommandHandler _handler;

        public DeleteEventCommandHandlerTests()
        {
            _eventRepositoryMock = new Mock<IEventRepository>();
            _validator = new DeleteEventCommandValidator();
            _handler = new DeleteEventCommandHandler(_eventRepositoryMock.Object, _validator);
        }

        [Fact]
        public async Task Handle_ShouldThrowValidationException_WhenInvalidDataProvided()
        {
            //Arrange
            var id = new Guid();

            //Act & Assert
            await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(new DeleteEventCommand(id), default));
        }

        [Fact]
        public async Task Handle_ShouldThrowNotFoundException_WhenNoEventWithProvidedIdFound()
        {
            //Arrange
            var id = new Guid("697c3b02-4073-44e5-b3fd-69d6226a1d5f");

            //Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(new DeleteEventCommand(id), default));
        }

        [Fact]
        public async Task Handle_ShouldExecuteDeletion_WhenEventWithGivenIdExists()
        {
            //Arrange
            var existingEvent = TestData.GetEvents().First();

            _eventRepositoryMock.Setup(m => m.GetByIdAsync(It.IsAny<Guid>(), null, default))
                .ReturnsAsync(existingEvent);

            _eventRepositoryMock.Setup(m => m.DeleteAsync(It.IsAny<Guid>(), default))
                .Returns(Task.CompletedTask);

            //Act
            var result = await _handler.Handle(new DeleteEventCommand(existingEvent.Id), default);

            //Assert
            Assert.Equal(Unit.Value, result);
        }
    }
}
