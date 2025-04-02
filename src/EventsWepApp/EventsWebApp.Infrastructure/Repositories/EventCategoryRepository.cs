using EventsWebApp.Domain.Entities;
using EventsWebApp.Domain.Interfaces;
using EventsWebApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsWebApp.Infrastructure.Repositories
{
    public class EventCategoryRepository : BaseRepository<EventCategory>, IEventCategoryRepository
    {
        public EventCategoryRepository(AppDbContext appDbContext) : base(appDbContext) { }

        public async Task<EventCategory> GetByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await _dbSet.FirstOrDefaultAsync(e => e.Name == name, cancellationToken);
        }
    }
}
