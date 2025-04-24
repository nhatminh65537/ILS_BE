using ILS_BE.Domain.DTOs;

namespace ILS_BE.Application.Interfaces
{
    public interface IContentItemService : IDataService<LearnNodeDTO>
    {
        public Task<ContentItemTreeDTO> GetModuleTreeByIdAsync(int id);
        public Task UpdateModuleTreeAsync(ContentItemTreeDTO moduleDetailDTO);
        public Task<LearnNodeDTO> AddModuleAsync(ModuleDTO moduleDto);
        public Task<LearnNodeDTO> AddLessonAsync(LessonDTO lessonDTO);
    }
}
