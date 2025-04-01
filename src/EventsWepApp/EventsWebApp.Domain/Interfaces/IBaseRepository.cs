using EventsWebApp.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsWebApp.Domain.Interfaces
{
    public interface IBaseRepository<T>
    {
        Task<PaginatedDTO<T>> GetAllASync(int pageNumber, int pageSize, Func<IQueryable<T>, IQueryable<T>> includeProperties = null, CancellationToken cancellationToken = default);
        Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);
        Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);    
    }
}
