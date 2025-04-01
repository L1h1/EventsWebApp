using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsWebApp.Application.DTOs
{
    public class EventRequestDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime EventDateAndTime { get; set; }
        public string EventAddress { get; set; }
        public Guid EventCategoryId { get; set; }
        public int MaxParticipantCount { get; set; }
        public string ImagePath { get; set; }
    }
}
