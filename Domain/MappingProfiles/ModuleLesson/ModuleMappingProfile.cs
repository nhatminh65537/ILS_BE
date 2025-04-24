using AutoMapper;
using ILS_BE.Domain.Models;
using ILS_BE.Domain.DTOs;

namespace ILS_BE.Domain.MappingProfiles
{
    public class ModuleMappingProfile : Profile
    {
        public ModuleMappingProfile()
        {
            CreateMap<LearnModule, ModuleDTO>()
                .ReverseMap()
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Tags, opt => opt.Ignore())
                .ForMember(dest => dest.NodeId, opt => opt.Ignore());
            CreateMap<LearnLifecycleState, LifecycleStateDTO>() 
                .ReverseMap()
                .ForMember(dest => dest.Name, opt => opt.Ignore());
            CreateMap<LearnTag, TagDTO>()
                .ReverseMap();
            CreateMap<LearnCategory, CategoryDTO>()
                .ReverseMap();
        }
    }
}
