using AutoMapper;
using ILS_BE.Domain.Models;
using ILS_BE.Domain.DTOs;

namespace ILS_BE.Domain.MappingProfiles
{
    public class ContentItemMappingProfile : Profile
    {
        public ContentItemMappingProfile()
        {
            CreateMap<LearnNode, LearnNodeDTO>()
                .ReverseMap()
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());
        }
    }
}
