using ILS_BE.Domain.DTOs;

namespace ILS_BE.Application.Interfaces
{
    public interface INodeService<NDTO>
    {
        public Task<TreeDTO<NDTO>> GetTreeAsync(int rootId);
        public Task UpdateTreeAsync(TreeDTO<NDTO> treeDTO);
    }
}
