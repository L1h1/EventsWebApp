using EventsWebApp.Domain.Entities;
using EventsWebApp.Domain.Interfaces;
using EventsWebApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EventsWebApp.Infrastructure.Repositories
{
    public class EventRepository : BaseRepository<Event>, IEventRepository
    {
        public EventRepository(AppDbContext appDbContext) : base(appDbContext) { }

        public async Task<IEnumerable<Event>> GetByFilterAsync(
            DateTime? eventDateAndTime = null,
            string eventAddress = null,
            Guid? categoryId = null,
            CancellationToken cancellationToken = default)
        {
            IQueryable<Event> query = _dbSet.Include(e=>e.EventCategory);

            if (eventDateAndTime != null)
                query = query.Where(e => e.EventDateAndTime == eventDateAndTime.Value);

            if (!string.IsNullOrEmpty(eventAddress))
                query = query.Where(e => e.EventAddress == eventAddress);

            if (categoryId != null)
                query = query.Where(e => e.EventCategoryId == categoryId.Value);

            return await query.ToListAsync(cancellationToken);
        }

        public async Task<Event> GetByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await _dbSet.Include(e=>e.EventCategory).FirstOrDefaultAsync(e=>e.Name == name, cancellationToken);
        }
    }
}
