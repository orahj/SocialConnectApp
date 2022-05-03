using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppAPI.DTOs;
using AppAPI.Entities;
using AppAPI.Extensions;
using AutoMapper;

namespace AppAPI.helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser, MemberDTO>()
            .ForMember(dest => dest.PhotoUrl, 
                opt => opt.MapFrom(src=>src.Photos.FirstOrDefault(x=>x.IsMain).Url))
            .ForMember(dest => dest.Age, opt=>opt.MapFrom(src =>src.DateOfBirth.CalculateAge()));
            CreateMap<Photo,PhotoDTO>();
        }
    }
}