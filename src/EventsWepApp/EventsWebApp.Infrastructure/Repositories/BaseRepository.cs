using EventsWebApp.Domain.Interfaces;
using EventsWebApp.Infrastructure.Data;
using EventsWebApp.Shared.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsWebApp.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly DbSet<T> _dbSet;
        protected readonly AppDbContext _context;

        public BaseRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var entity = await GetByIdAsync(id);
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<PaginatedDTO<T>> GetAllASync(int pageNumber, int pageSize, Func<IQueryable<T>, IQueryable<T>> includeProperties = null, CancellationToken cancellationToken = default)
        {
            IQueryable<T> query = _dbSet;

            if (includeProperties != null)
            {
                query = includeProperties(query);
            }

            var totalCount = await query.CountAsync(cancellationToken);
            var items = await query
                .Skip(pageNumber - 1)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return new PaginatedDTO<T> { 
                Items = items,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize 
            };
        }

        public async Task<T> GetByIdAsync(Guid id, Func<IQueryable<T>, IQueryable<T>> includeProperties = null, CancellationToken cancellationToken = default)
        {
            IQueryable<T> query = _dbSet;

            if (includeProperties != null)
            {
                query = includeProperties(query);
            }

            return await query.FirstOrDefaultAsync(e => EF.Property<Guid>(e, "Id") == id, cancellationToken);
        }

        public async Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return entity;
        }
    }
}
