using ILS_BE.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ILS_BE.Infrastructure.Repositories
{
    public class ModuleRepository : GenericRepository<Module>
    {
        public ModuleRepository(DbContext context) : base(context)
        {
        }

        public override async Task<List<Module>> GetAllAsync()
        {
            try
            {
                return await _dbSet
                    .Include(m => m.Category)
                    .Include(m => m.LifecycleState)
                    .Include(m => m.Tags)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                // Log exception
                throw new Exception("Could not retrieve entities", ex);
            }
        }

        public override async Task<Module?> GetByIdAsync(int id)
        {
            try
            {
                return await _dbSet
                    .Include(m => m.Category)
                    .Include(m => m.LifecycleState)
                    .Include(m => m.Tags)
                    .FirstOrDefaultAsync(m => m.Id == id);
            }
            catch (Exception ex)
            {
                // Log exception
                throw new Exception($"Could not retrieve entity with id {id}", ex);
            }
        }

        public override async Task<Module?> GetFirstWhereAsync(Expression<Func<Module, bool>> expression)
        {
            try
            {
                return await _dbSet
                    .Include(m => m.Category)
                    .Include(m => m.LifecycleState)
                    .Include(m => m.Tags)
                    .FirstOrDefaultAsync(expression);
            }
            catch (Exception ex)
            {
                // Log exception
                throw new Exception("Could not retrieve entities", ex);
            }
        }

        public override List<Module> GetAll()
        {
            try
            {
                return _dbSet
                    .Include(m => m.Category)
                    .Include(m => m.LifecycleState)
                    .Include(m => m.Tags)
                    .ToList();
            }
            catch (Exception ex)
            {
                // Log exception
                throw new Exception("Could not retrieve entities", ex);
            }
        }
        public override Module? GetById(int id)
        {
            try
            {
                return _dbSet
                    .Include(m => m.Category)
                    .Include(m => m.LifecycleState)
                    .Include(m => m.Tags)
                    .FirstOrDefault(m => m.Id == id);
            }
            catch (Exception ex)
            {
                // Log exception
                throw new Exception($"Could not retrieve entity with id {id}", ex);
            }
        }

        public override Module? GetFirstWhere(Expression<Func<Module, bool>> expression)
        {
            try
            {
                return _dbSet
                    .Include(m => m.Category)
                    .Include(m => m.LifecycleState)
                    .Include(m => m.Tags)
                    .FirstOrDefault(expression);
            }
            catch (Exception ex)
            {
                // Log exception
                throw new Exception("Could not retrieve entities", ex);
            }
        }
    }
}
