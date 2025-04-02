using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsWebApp.Domain.Interfaces
{
    public interface IPhotoService
    {
        Task<Stream> GetPhoto(Guid id);
        Task UploadPhoto(Guid id, Stream fileStream);
    }
}
