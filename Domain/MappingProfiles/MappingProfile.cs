using AutoMapper;
using ILS_BE.Domain.DTOs;
using ILS_BE.Domain.Interfaces;
using ILS_BE.Domain.Models;

namespace ILS_BE.Domain.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PaginatedResult<ChallengeNode>, PaginatedResult<ChallengeNodeDTO>>();
            CreateMap<PaginatedResult<LearnModule>, PaginatedResult<LearnModuleDTO>>();

            CreateMap<BaseEntity<int>, BaseDTO<int>>()
                .ReverseMap()
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());
        }
    }
}

