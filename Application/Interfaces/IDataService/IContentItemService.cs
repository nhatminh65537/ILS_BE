using ILS_BE.Domain.DTOs;

namespace ILS_BE.Application.Interfaces
{
    public interface IContentItemService
    {
        public Task<ContentItemTreeDTO> GetModuleTreeByIdAsync(int id);
        public Task UpdateModuleTreeAsync(ContentItemTreeDTO moduleDetailDTO);
    }
}
