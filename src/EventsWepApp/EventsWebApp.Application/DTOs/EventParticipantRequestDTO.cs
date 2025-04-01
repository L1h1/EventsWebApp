using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsWebApp.Application.DTOs
{
    public class EventParticipantRequestDTO
    {
        public Guid EventId { get; set; }
        public Guid UserId { get; set; }
    }
}
