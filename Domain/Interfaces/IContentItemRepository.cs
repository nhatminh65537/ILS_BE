using ILS_BE.Domain.Models;

namespace ILS_BE.Domain.Interfaces
{
    public interface IContentItemRepository : IGenericRepository<ContentItem>
    {
        public Task<List<ContentItem>> GetContentItemsInModuleAsync(int moduleId);
        public List<ContentItem> GetContentItemsInModule(int moduleId);
    }
}
