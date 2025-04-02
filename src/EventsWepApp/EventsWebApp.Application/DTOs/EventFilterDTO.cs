using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsWebApp.Application.DTOs
{
    public class EventFilterDTO
    {
        public DateTime? EventDateAndTime { get; set; }
        public string? EventAddress { get; set; }
        public Guid? CategoryId { get; set; }
    }
}
