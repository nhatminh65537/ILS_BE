using ILS_BE.Domain.Models;

namespace ILS_BE.Domain.Interfaces
{
    public interface IContentItemRepository : IGenericRepository<LearnNode>
    {
        public Task<List<LearnNode>> GetContentItemsInModuleAsync(int moduleId);
        public List<LearnNode> GetContentItemsInModule(int moduleId);
    }
}
