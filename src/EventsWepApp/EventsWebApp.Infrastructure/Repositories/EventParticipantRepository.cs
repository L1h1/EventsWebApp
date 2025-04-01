using EventsWebApp.Domain.Entities;
using EventsWebApp.Domain.Interfaces;
using EventsWebApp.Infrastructure.Data;
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
    }
}
