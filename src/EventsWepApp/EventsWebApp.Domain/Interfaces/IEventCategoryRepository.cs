using EventsWebApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsWebApp.Domain.Interfaces
{
    public interface IEventCategoryRepository : IBaseRepository<EventCategory>
    {
    }
}
