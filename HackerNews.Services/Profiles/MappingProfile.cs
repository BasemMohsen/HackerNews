using AutoMapper;
using HackerNews.Services.Dto;
using HackerNews.ApiClient.Models;

namespace HackerNews.Services.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Story, StoryDto>()
                .ForMember(dest => dest.Time, opt => opt.MapFrom(src => DateTimeOffset.FromUnixTimeSeconds(src.Time).ToString("o")));
        }
    }
}
