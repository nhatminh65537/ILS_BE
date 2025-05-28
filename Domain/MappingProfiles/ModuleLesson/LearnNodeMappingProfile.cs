using AutoMapper;
using ILS_BE.Domain.Models;
using ILS_BE.Domain.DTOs;

namespace ILS_BE.Domain.MappingProfiles
{
    public class LearnNodeMappingProfile : Profile
    {
        public LearnNodeMappingProfile()
        {
            CreateMap<LearnNode, LearnNodeDTO>()
                .ReverseMap()
                .ForMember(dest => dest.Lesson, opt => opt.Ignore());
            CreateMap<LearnNodeCreateOrUpdateDTO, LearnNode>();
        }
    }
}
