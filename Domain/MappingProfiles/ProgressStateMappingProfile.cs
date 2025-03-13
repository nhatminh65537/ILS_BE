using AutoMapper;
using ILS_BE.Domain.Models;
using ILS_BE.Domain.DTOs;

namespace ILS_BE.Domain.MappingProfiles
{
    public class ProgressStateMappingProfile : Profile
    {
        public ProgressStateMappingProfile()
        {
            CreateMap<ProgressState, ProgressStateDTO>()
                .ReverseMap()
                .ForMember(dest => dest.Name, opt => opt.Ignore());
        }
    }
}
