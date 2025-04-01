using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsWebApp.Domain.Entities
{
    public class Event
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime EventDateAndTime { get; set; }
        public string EventAddress { get; set; }
        public Guid EventCategoryId { get; set; }
        public EventCategory EventCategory { get; set; }
        public int MaxParticipantCount { get; set; }
        public IEnumerable<EventParticipant> Participants { get; set; }
        public string ImagePath { get; set; }
    }
}
