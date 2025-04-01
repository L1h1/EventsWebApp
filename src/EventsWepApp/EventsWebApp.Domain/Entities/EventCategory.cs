using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsWebApp.Domain.Entities
{
    public class EventCategory
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Event> Events { get; set; }
    }
}
