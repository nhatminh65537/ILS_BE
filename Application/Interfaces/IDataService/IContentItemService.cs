using ILS_BE.Domain.DTOs;

namespace ILS_BE.Application.Interfaces
{
    public interface IContentItemService : IDataService<ContentItemDTO>
    {
        public Task<ContentItemTreeDTO> GetModuleTreeByIdAsync(int id);
        public Task UpdateModuleTreeAsync(ContentItemTreeDTO moduleDetailDTO);
        public Task<ContentItemDTO> AddModuleAsync(ModuleDTO moduleDto);
        public Task<ContentItemDTO> AddLessonAsync(LessonDTO lessonDTO);
    }
}
