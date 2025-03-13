using AutoMapper;
using ILS_BE.Domain.Models;
using ILS_BE.Domain.DTOs;

namespace ILS_BE.Domain.MappingProfiles
{
    public class RoleMappingProfile : Profile
    {
        public RoleMappingProfile()
        {
            CreateMap<Role, RoleDTO>()
                .ReverseMap()
                .ForMember(dest => dest.Changeable, opt => opt.Ignore());
        }
    }
}
