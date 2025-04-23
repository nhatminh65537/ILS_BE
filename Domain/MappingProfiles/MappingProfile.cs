using AutoMapper;
using ILS_BE.Domain.DTOs;
using ILS_BE.Domain.Interfaces;
using ILS_BE.Domain.Models;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<PaginatedResult<Module>, PaginatedResult<ModuleDTO>>();
        // Add other mappings here
    }
}
