using ILS_BE.Domain.DTOs;

namespace ILS_BE.Application.Interfaces
{
    public interface INodeService<NDTO>
    {
        public Task<TreeDTO<LearnNodeDTO>> GetTreeByIdAsync(int id);
        public Task UpdateTreeAsync(TreeDTO<NDTO> moduleDetailDTO);
    }
}
