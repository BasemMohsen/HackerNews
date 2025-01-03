using AutoMapper;
using HackerNews.Services.Dto;
using HackerNews.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerNews.Services.Profiles
{
    public class StoryProfile : Profile
    {
        public StoryProfile()
        {
            CreateMap<Story, StoryDto>()
                .ForMember(dest => dest.Time, opt => opt.MapFrom(src => DateTimeOffset.FromUnixTimeSeconds(src.Time).ToString("o")));
        }
    }
}
