using ILS_BE.Domain.Interfaces;
using ILS_BE.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ILS_BE.Infrastructure.Repositories
{
    public class ContentItemRepository : GenericRepository<ContentItem>, IContentItemRepository
    {
        public ContentItemRepository(DbContext context) : base(context)
        {
        }
        public async Task<List<ContentItem>> GetContentItemsInModuleAsync(int moduleId)
        {
            var rootContainItem = await _dbSet
                .Include(ci => ci.Module).ThenInclude(m => m.Category)
                .Include(ci => ci.Module).ThenInclude(m => m.LifecycleState)
                .Include(ci => ci.Module).ThenInclude(m => m.Tags)
                .FirstOrDefaultAsync(ci => ci.ModuleId == moduleId)
                ?? throw new Exception("Module not found");
            var rootPath = rootContainItem.Path + rootContainItem.Id.
                ToString() + ".";
            var ContentItems = await _dbSet.Where(ci => ci.Path.StartsWith(rootPath))
                .Include(ci => ci.Lesson)
                .ThenInclude(l => l.LessonType)
                .ToListAsync();
            ContentItems.Insert(0, rootContainItem);
            return ContentItems;

        }
        public List<ContentItem> GetContentItemsInModule(int moduleId)
        {
            var rootContainItem = _dbSet
                .Include(ci => ci.Module).ThenInclude(m => m.Category)
                .Include(ci => ci.Module).ThenInclude(m => m.LifecycleState)
                .Include(ci => ci.Module).ThenInclude(m => m.Tags)
                .FirstOrDefault(ci => ci.ModuleId == moduleId)
                ?? throw new Exception("Module not found");
            var rootPath = rootContainItem.Path + rootContainItem.Id.
                ToString() + ".";
            var ContentItems = _dbSet.Where(ci => ci.Path.StartsWith(rootPath))
                .Include(ci => ci.Lesson)
                .ThenInclude(l => l.LessonType)
                .ToList();
            ContentItems.Insert(0, rootContainItem);
            return ContentItems;
        }
    }
}
