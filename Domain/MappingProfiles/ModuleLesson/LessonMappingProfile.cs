using AutoMapper;
using ILS_BE.Domain.DTOs;
using ILS_BE.Domain.Models;

namespace ILS_BE.Domain.MappingProfiles
{
    public class LessonMappingProfile : Profile
    {
        public LessonMappingProfile()
        {
            CreateMap<Lesson, LessonDTO>()
                .ReverseMap()
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.ContentItemId, opt => opt.Ignore());
            CreateMap<Lesson, LessonContentDTO>()
                .ReverseMap();
            CreateMap<LessonType, LessonTypeDTO>()
                .ReverseMap()
                .ForMember(dest => dest.Name, opt => opt.Ignore());
        }
    }
}
