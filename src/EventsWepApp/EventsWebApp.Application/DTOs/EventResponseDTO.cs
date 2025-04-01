using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsWebApp.Application.DTOs
{
    public class EventResponseDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime EventDateAndTime { get; set; }
        public string EventAddress { get; set; }
        public string CategoryName { get; set; }
        public int MaxParticipantCount { get; set; }
        public IEnumerable<EventParticipantResponseDTO> Participants { get; set; }
        public string ImagePath { get; set; }
    }
}
