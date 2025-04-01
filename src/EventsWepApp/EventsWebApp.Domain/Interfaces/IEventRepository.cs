using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventsWebApp.Domain.Entities;

namespace EventsWebApp.Domain.Interfaces
{
    public interface IEventRepository : IBaseRepository<Event>
    {
        Task<Event> GetByNameAsync(string name, CancellationToken cancellationToken = default);
        Task<IEnumerable<Event>> GetByFilterAsync(
            DateTime? eventDateAndTime = null,
            string eventAddress = null,
            Guid? eventCategoryId = null,
            CancellationToken cancellationToken = default);
    }
}
