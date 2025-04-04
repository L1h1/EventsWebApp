using EventsWebApp.Domain.Interfaces;
using EventsWebApp.Infrastructure.Data;
using EventsWebApp.Infrastructure.Repositories;
using EventsWebApp.Tests.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsWebApp.Tests.Event.Repository
{
    public class EventRepostoryTests
    {
        private readonly AppDbContext _context;
        private readonly IEventRepository _eventRepository;

        public EventRepostoryTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            _context = new AppDbContext(options);
            _eventRepository = new EventRepository(_context);

            _context.Events.AddRange(TestData.GetEvents());
            _context.Users.AddRange(TestData.GetUsers());
            _context.EventParticipants.AddRange(TestData.GetEventsParticipant());
            _context.SaveChanges();
        }

        [Fact]
        public async Task AddAsync_AddEventToDatabase()
        {
            //Arrange
            var newEvent = new Domain.Entities.Event
            {
                Name = "WEEEEEEEEEEEEE",
                Description = "...",
                EventDateAndTime = DateTime.Now,
                EventAddress = "...",
                EventCategoryId = TestData.GetCategories().First().Id,
                MaxParticipantCount = 20,
                ImagePath = "PLACEHOLDER"
            };

            //Act 
            var addedEvent = await _eventRepository.AddAsync(newEvent, default);
            var eventFromDb = await _context.Events.FindAsync(addedEvent.Id);

            //Assert
            Assert.NotNull(addedEvent);
            Assert.NotNull(eventFromDb);
            Assert.Equal(newEvent.Name, addedEvent.Name);
            Assert.Equal(newEvent.Description, addedEvent.Description);
            Assert.Equal(newEvent.EventDateAndTime, addedEvent.EventDateAndTime, TimeSpan.FromSeconds(1));
            Assert.Equal(newEvent.EventAddress, addedEvent.EventAddress);
            Assert.Equal(newEvent.EventCategoryId, addedEvent.EventCategoryId);
            Assert.Equal(newEvent.MaxParticipantCount, addedEvent.MaxParticipantCount);
            Assert.Equal(newEvent.ImagePath, addedEvent.ImagePath);
        }

        [Fact]
        public async Task GetByNameAsync_ReturnsEvent_WhenEventExists()
        {
            //Arrange
            var existingEvent = TestData.GetEvents().First();

            //Act
            var eventFromDb = await _eventRepository.GetByNameAsync(existingEvent.Name, default);

            //Assert
            Assert.NotNull(eventFromDb);
            Assert.Equal(existingEvent.Name, eventFromDb.Name);
            Assert.Equal(existingEvent.Description, eventFromDb.Description);
            Assert.Equal(existingEvent.EventDateAndTime, eventFromDb.EventDateAndTime, TimeSpan.FromSeconds(1));
            Assert.Equal(existingEvent.EventAddress, eventFromDb.EventAddress);
            Assert.Equal(existingEvent.EventCategoryId, eventFromDb.EventCategoryId);
            Assert.Equal(existingEvent.MaxParticipantCount, eventFromDb.MaxParticipantCount);
            Assert.Equal(existingEvent.ImagePath, eventFromDb.ImagePath);
        }

        [Fact]
        public async Task GetByNameAsync_ReturnsNull_WhenEventDoesNotExist()
        {
            //Arrange
            var name = "HDGFK:UDZKFH:DSKKFSDF:D";

            //Act
            var eventFromDb = await _eventRepository.GetByNameAsync(name, default);

            //Assert
            Assert.Null(eventFromDb);
        }
    }
}
