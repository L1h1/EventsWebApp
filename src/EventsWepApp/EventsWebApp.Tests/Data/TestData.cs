using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsWebApp.Tests.Data
{
    internal static class TestData
    {
        internal static List<Domain.Entities.User> GetUsers()
        {
            return new List<Domain.Entities.User>
            {
                new Domain.Entities.User
                {
                    Id = new Guid("4af05f23-445a-47da-baab-abfbea42bee5"),
                    Role = Domain.Enums.UserRole.User,
                    FirstName = "User",
                    LastName = "User",
                    DateOfBirth = DateOnly.FromDateTime(DateTime.Now),
                    Email = "user@gmail.com",
                    Password = "Password"
                },
                new Domain.Entities.User
                {
                    Id = new Guid("d4175965-5ac6-4755-9a06-d07518412f5d"),
                    Role = Domain.Enums.UserRole.Admin,
                    FirstName = "Admin",
                    LastName = "Admin",
                    DateOfBirth = DateOnly.FromDateTime(DateTime.Now),
                    Email = "admin@gmail.com",
                    Password = "drowssaP"
                }
            };
        }

        internal static List<Domain.Entities.EventCategory> GetCategories()
        {
            return new List<Domain.Entities.EventCategory>
            {
                new Domain.Entities.EventCategory
                {
                    Id = new Guid("a0617827-0b58-405b-9e59-17f3f00e7ce7"),
                    Name = "Category 1"
                },
                new Domain.Entities.EventCategory
                {
                    Id = new Guid("b375f580-c6bd-464a-ba6d-9476bf89d6d4"),
                    Name = "Category 2"
                }
            };
        }

        internal static List<Domain.Entities.Event> GetEvents()
        {
            return new List<Domain.Entities.Event>
            {
                new Domain.Entities.Event
                {
                    Id = new Guid("1604be82-8008-45e1-bd61-5c4a44717746"),
                    Name = "Test event HAHAHAH",
                    Description = "This one for category 1",
                    EventDateAndTime = DateTime.Now,
                    EventAddress = "Somewhere?",
                    EventCategory = GetCategories().First(),
                    EventCategoryId = GetCategories().First().Id,
                    MaxParticipantCount = 100,
                    ImagePath = "TEST"
                },
                new Domain.Entities.Event
                {
                    Id = new Guid("490062f5-81b3-46ae-b745-eeea7a6c783a"),
                    Name = "Test event?",
                    Description = "This one for category 2",
                    EventDateAndTime = DateTime.Now,
                    EventAddress = "Somewhere...",
                    EventCategory = GetCategories().Last(),
                    EventCategoryId = GetCategories().Last().Id,
                    MaxParticipantCount = 100,
                    ImagePath = "TEST"
                }
            };
        }

        internal static List<Domain.Entities.EventParticipant> GetEventsParticipant()
        {
            return new List<Domain.Entities.EventParticipant>
            {
                new Domain.Entities.EventParticipant
                {
                    Id = new Guid("1ad460f9-a3a9-4776-a90f-7d99a79825d8"),
                    UserId = new Guid("4af05f23-445a-47da-baab-abfbea42bee5"),
                    EventId = new Guid("1604be82-8008-45e1-bd61-5c4a44717746")
                }
            };
        }
    }
}
