// Application/Mapping/ChallengeProfile.cs
using AutoMapper;
using ILS_BE.Domain.DTOs;
using ILS_BE.Domain.Models;

namespace ILS_BE.Application.Mapping
{
    public class ChallengeProfile : Profile
    {
        public ChallengeProfile()
        {
            CreateMap<ChallengeNode, ChallengeNodeDTO>();
            CreateMap<ChallengeNodeCreateOrUpdateDTO, ChallengeNode>();
            
            CreateMap<ChallengeProblem, ChallengeProblemDTO>();
            CreateMap<ChallengeProblemCreateOrUpdateDTO, ChallengeProblem>();
            
            CreateMap<ChallengeCategory, ChallengeCategoryDTO>().ReverseMap();
            CreateMap<ChallengeFile, ChallengeFileDTO>().ReverseMap();
            CreateMap<ChallengeTag, ChallengeTagDTO>().ReverseMap();
            CreateMap<ChallengeProblemTag, ChallengeProblemTag>();
            CreateMap<ChallengeWriteup, ChallengeWriteupDTO>().ReverseMap();
            CreateMap<ChallengeState, ChallengeStateDTO>().ReverseMap();
        }
    }
}
