using AutoMapper;
using ILS_BE.Domain.DTOs;
using ILS_BE.Domain.Models;

namespace ILS_BE.Domain.MappingProfiles
{
    public class LessonMappingProfile : Profile
    {
        public LessonMappingProfile()
        {
            CreateMap<LearnLesson, LessonDTO>()
                .ReverseMap()
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.NodeId, opt => opt.Ignore());
            CreateMap<LearnLesson, LessonContentDTO>()
                .ReverseMap();
            CreateMap<LearnLessonType, LessonTypeDTO>()
                .ReverseMap()
                .ForMember(dest => dest.Name, opt => opt.Ignore());
        }
    }
}
