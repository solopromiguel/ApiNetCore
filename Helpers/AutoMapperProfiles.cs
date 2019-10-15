using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication21.Dtos;
using WebApplication21.sakila;

namespace WebApplication21.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Users, UserForListDto>()
            .ForMember(dest => dest.PhotoUrl, opt => {
                opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url);
            })
            .ForMember(dest => dest.Age, opt => {
                opt.MapFrom(d => d.DateOfBirth.CalculateAge());
            });
            //CreateMap<Users, UserForDetailedDto>()
            //    .ForMember(dest => dest.PhotoUrl, opt => {
            //        opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url);
            //    })
            //    .ForMember(dest => dest.Age, opt => {
            //        opt.MapFrom(d => d.DateOfBirth.CalculateAge());
            //    });
        }
    }
}
