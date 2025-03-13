using AutoMapper;
using ILS_BE.Domain.DTOs;
using ILS_BE.Domain.Models;

namespace ILS_BE.Domain.MappingProfiles
{
    public class UserProfileMappingProfile : Profile
    {
        public UserProfileMappingProfile() {
            CreateMap<UserProfile, UserProfileDTO>()
                .ReverseMap()
                .ForMember(dest => dest.Xp, opt => opt.Ignore())
                .ForMember(dest => dest.Level, opt => opt.Ignore());
        }
    }
}
