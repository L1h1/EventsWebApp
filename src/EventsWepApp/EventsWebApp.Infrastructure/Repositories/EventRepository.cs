﻿using EventsWebApp.Domain.Entities;
using EventsWebApp.Domain.Interfaces;
using EventsWebApp.Infrastructure.Data;
using EventsWebApp.Shared.DTO;
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

        public async Task<PaginatedDTO<Event>> GetByFilterAsync(
            int pageNumber,
            int pageSize,
            DateTime? eventDateAndTime = null,
            string eventAddress = null,
            Guid? categoryId = null,
            CancellationToken cancellationToken = default)
        {
            IQueryable<Event> query = _dbSet
                .Include(e=>e.EventCategory)
                .Include(e => e.Participants)
                    .ThenInclude(p => p.User);

            if (eventDateAndTime != null)
                query = query.Where(e => e.EventDateAndTime == eventDateAndTime.Value);

            if (!string.IsNullOrEmpty(eventAddress))
                query = query.Where(e => e.EventAddress.Contains(eventAddress));

            if (categoryId != null)
                query = query.Where(e => e.EventCategoryId == categoryId.Value);

            var totalCount = await query.CountAsync(cancellationToken);
            var items = await query
                .Skip(pageNumber - 1)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return new PaginatedDTO<Event>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<Event> GetByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Include(e=>e.EventCategory)
                .Include(e=>e.Participants)
                    .ThenInclude(p => p.User)
                .FirstOrDefaultAsync(e=>e.Name == name, cancellationToken);
        }
    }
}
