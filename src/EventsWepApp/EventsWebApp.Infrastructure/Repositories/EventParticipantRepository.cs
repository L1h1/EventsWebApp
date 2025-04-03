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
    public class EventParticipantRepository : BaseRepository<EventParticipant>, IEventParticipantRepository
    {
        public EventParticipantRepository(AppDbContext appDbContext) : base(appDbContext) { }

        public async Task<bool> CheckParticipation(Guid userId, Guid eventId, CancellationToken cancellationToken = default)
        {
            return await _context.EventParticipants
                .AnyAsync(p => p.UserId == userId && p.EventId == eventId);
        }
    }
}
