using AutoMapper;
using ILS_BE.Domain.Models;
using ILS_BE.Domain.DTOs;

namespace ILS_BE.Domain.MappingProfiles
{
    public class ProgressStateMappingProfile : Profile
    {
        public ProgressStateMappingProfile()
        {
            CreateMap<LearnProgressState, ProgressStateDTO>()
                .ReverseMap()
                .ForMember(dest => dest.Name, opt => opt.Ignore());
        }
    }
}
