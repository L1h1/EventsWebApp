﻿using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsWebApp.Application.Commands.Photo.UploadEventPhotoCommand
{
    public sealed record UploadEventPhotoCommand(Guid eventId, IFormFile file) : IRequest<Unit>;
}
