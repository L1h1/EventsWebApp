using EventsWebApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsWebApp.Infrastructure.Services
{
    public class PhotoService : IPhotoService
    {
        private const string RootPath = "Uploads";

        public async Task<Stream> GetPhoto(Guid id)
        {
            var location = $"events/{id}";
            var filePath = Path.Combine(RootPath, location, $"{id}.jpg");

            if (!File.Exists(filePath))
                filePath = Path.Combine(RootPath, "default.jpg");

            return File.OpenRead(filePath);
        }

        public async Task UploadPhoto(Guid id, Stream fileStream)
        {
            var location = $"events/{id}";
            var productDir = Path.Combine(RootPath, location);
            Directory.CreateDirectory(productDir);
            var filePath = Path.Combine(productDir, $"{id}.jpg");

            if (File.Exists(filePath))
                File.Delete(filePath);

            await using var file = File.Create(filePath);
            await fileStream.CopyToAsync(file);
        }
    }
}
