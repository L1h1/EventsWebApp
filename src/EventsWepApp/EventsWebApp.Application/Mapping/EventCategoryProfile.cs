using AutoMapper;
using EventsWebApp.Application.DTOs;
using EventsWebApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsWebApp.Application.Mapping
{
    public class EventCategoryProfile : Profile
    {
        public EventCategoryProfile()
        {
            CreateMap<EventCategory, EventCategoryResponseDTO>();
        }
    }
}
