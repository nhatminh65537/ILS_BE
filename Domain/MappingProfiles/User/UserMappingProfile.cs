using AutoMapper;
using ILS_BE.Domain.Models;
using ILS_BE.Domain.DTOs;

namespace ILS_BE.Domain.MappingProfiles
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, UserDTO>()
                .ReverseMap()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.UserName, opt => opt.Ignore())
                .ForMember(dest => dest.Email, opt => opt.Ignore())
                .ForMember(dest => dest.EmailVerified, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());
            CreateMap<User, UserPublicDTO>()
                .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Profile.DisplayName))
                .ForMember(dest => dest.AvatarPath, opt => opt.MapFrom(src => src.Profile.AvatarPath))
                .ForMember(dest => dest.Xp, opt => opt.MapFrom(src => src.Profile.Xp))
                .ForMember(dest => dest.Level, opt => opt.MapFrom(src => src.Profile.Level))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
        }
    }
}
