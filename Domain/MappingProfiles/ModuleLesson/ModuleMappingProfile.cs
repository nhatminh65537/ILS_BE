using AutoMapper;
using ILS_BE.Domain.Models;
using ILS_BE.Domain.DTOs;

namespace ILS_BE.Domain.MappingProfiles
{
    public class ModuleMappingProfile : Profile
    {
        public ModuleMappingProfile()
        {
            CreateMap<Module, ModuleDTO>()
                .ReverseMap()
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Tags, opt => opt.Ignore())
                .ForMember(dest => dest.ContentItemId, opt => opt.Ignore());
            CreateMap<LifecycleState, LifecycleStateDTO>() 
                .ReverseMap()
                .ForMember(dest => dest.Name, opt => opt.Ignore());
            CreateMap<Tag, TagDTO>()
                .ReverseMap();
            CreateMap<Category, CategoryDTO>()
                .ReverseMap();
        }
    }
}
