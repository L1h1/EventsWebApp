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
    public class EventProfile : Profile
    {
        public EventProfile()
        {
            CreateMap<EventRequestDTO, Event>();

            CreateMap<Event, EventResponseDTO>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.EventCategory.Name));
        }
    }
}
