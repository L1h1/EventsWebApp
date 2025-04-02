using AutoMapper;
using EventsWebApp.Application.DTOs;
using EventsWebApp.Domain.Entities;
using EventsWebApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsWebApp.Application.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserRequestDTO, User>();

            CreateMap<UserRole, string>().ConvertUsing(u => u.ToString());

            CreateMap<User, UserResponseDTO>();
        }
    }
}
