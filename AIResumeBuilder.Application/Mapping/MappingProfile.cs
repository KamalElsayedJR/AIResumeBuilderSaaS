using AIResumeBuilder.Application.Dtos;
using AIResumeBuilder.Application.Dtos.Auth;
using AIResumeBuilder.Application.Dtos.Resume;
using AIResumeBuilder.Application.Dtos.Resume.Experience;
using AIResumeBuilder.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeBuilder.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, LoginResponse>();
            CreateMap<Resume, ResumeDto>().ReverseMap();
            CreateMap<AddExperienceDto, Experience>();
            CreateMap<Experience, ExperienceDto>().ReverseMap();
        }
    }
}
