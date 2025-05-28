using AutoMapper;
using ILS_BE.Domain.DTOs;
using ILS_BE.Domain.Models;

namespace ILS_BE.Domain.MappingProfiles
{
    public class LessonMappingProfile : Profile
    {
        public LessonMappingProfile()
        {
            CreateMap<LearnLesson, LearnLessonNodeDTO>();
            CreateMap<LearnLessonCreateOrUpdateDTO, LearnLesson>();
            CreateMap<LearnLesson, LearnLessonDTO>();

            CreateMap<LearnLessonType, LearnLessonTypeDTO>()
                .ReverseMap()
                .ForMember(dest => dest.Name, opt => opt.Ignore());
        }
    }
}
