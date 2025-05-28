using AutoMapper;
using ILS_BE.Domain.Models;
using ILS_BE.Domain.DTOs;
using Microsoft.AspNetCore.Identity;

namespace ILS_BE.Domain.MappingProfiles
{
    public class ModuleMappingProfile : Profile
    {
        public ModuleMappingProfile()
        {
            CreateMap<LearnModule, LearnModuleDTO>();
            CreateMap<LearnModuleCreateOrUpdateDTO, LearnModule>()
                .ForMember(dest => dest.Xp, opt => opt.Ignore())
                .ForMember(dest => dest.Duration, opt => opt.Ignore());

            CreateMap<LearnLifecycleState, LearnLifecycleStateDTO>() 
                .ReverseMap()
                .ForMember(dest => dest.Name, opt => opt.Ignore());
            CreateMap<LearnTag, LearnTagDTO>()
                .ReverseMap();
            CreateMap<LearnCategory, LearnCategoryDTO>()
                .ReverseMap();
        }
    }
}
