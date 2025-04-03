using EventsWebApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsWebApp.Domain.Interfaces
{
    public interface ITokenService
    {
        string CreateAccessToken(User user, CancellationToken cancellationToken = default);
        Tuple<string,int> CreateRefreshToken(CancellationToken cancellationToken = default);
    }
}
