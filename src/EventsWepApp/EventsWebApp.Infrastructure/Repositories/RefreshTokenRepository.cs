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
    public class RefreshTokenRepository : BaseRepository<RefreshToken>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(AppDbContext context) : base(context) { }

        public async Task<RefreshToken> GetByTokenString(string token, CancellationToken cancellationToken = default)
        {
            return await _context.RefreshTokens.Include(t => t.User).FirstOrDefaultAsync(t => t.Token == token);
        }
    }
}
