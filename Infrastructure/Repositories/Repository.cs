using ILS_BE.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ILS_BE.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public Repository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public virtual async Task<List<T>> GetAllAsync()
        {
            try
            {
                return await _dbSet.ToListAsync();
            }
            catch (Exception ex)
            {
                // Log exception
                throw new Exception("Could not retrieve entities", ex);
            }
        }

        public virtual async Task<List<T>> GetWhereAsync(Expression<Func<T, bool>> expression)
        {
            try
            {
                return await _dbSet.Where(expression).ToListAsync();
            }
            catch (Exception ex)
            {
                // Log exception
                throw new Exception("Could not retrieve entities", ex);
            }
        }

        public virtual async Task<T?> GetByIdAsync(int id)
        {
            try
            {
                return await _dbSet.FindAsync(id);
            }
            catch (Exception ex)
            {
                // Log exception
                throw new Exception($"Could not retrieve entity with id {id}", ex);
            }
        }

        public virtual async Task<T?> GetFirstWhereAsync(Expression<Func<T, bool>> expression)
        {
            try
            {
                return await _dbSet.FirstOrDefaultAsync(expression);
            }
            catch (Exception ex)
            {
                // Log exception
                throw new Exception("Could not retrieve entity", ex);
            }
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            try
            {
                var result = await _dbSet.AddAsync(entity);
                return result.Entity;
            }
            catch (Exception ex)
            {
                // Log exception
                throw new Exception("Could not add entity", ex);
            }
        }

        public virtual async Task UpdateAsync(T entity)
        {
            try
            {
                _dbSet.Attach(entity);
                _context.Entry(entity).State = EntityState.Modified;
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                // Log exception
                throw new Exception("Could not update entity", ex);
            }
        }

        public virtual async Task DeleteAsync(int id)
        {
            try
            {
                var entity = await _dbSet.FindAsync(id) ?? throw new Exception($"Entity with id {id} not found");
                _dbSet.Remove(entity);
            }
            catch (Exception ex)
            {
                // Log exception
                throw new Exception($"Could not delete entity with id {id}", ex);
            }
        }

        public virtual async Task DeleteWhereAsync(Expression<Func<T, bool>> expression)
        {
            try
            {
                var entities = await _dbSet.Where(expression).ToListAsync();
                _dbSet.RemoveRange(entities);
            }
            catch (Exception ex)
            {
                // Log exception
                throw new Exception("Could not delete entities", ex);
            }
        }

        public virtual async Task SaveAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log exception
                throw new Exception("Could not save changes", ex);
            }
        }

        public virtual List<T> GetAll()
        {
            try
            {
                return _dbSet.ToList();
            }
            catch (Exception ex)
            {
                // Log exception
                throw new Exception("Could not retrieve entities", ex);
            }
        }

        public virtual List<T> GetWhere(Expression<Func<T, bool>> expression)
        {
            try
            {
                return _dbSet.Where(expression).ToList();
            }
            catch (Exception ex)
            {
                // Log exception
                throw new Exception("Could not retrieve entities", ex);
            }
        }

        public virtual T? GetById(int id)
        {
            try
            {
                return _dbSet.Find(id);
            }
            catch (Exception ex)
            {
                // Log exception
                throw new Exception($"Could not retrieve entity with id {id}", ex);
            }
        }

        public virtual T? GetFirstWhere(Expression<Func<T, bool>> expression)
        {
            try
            {
                return _dbSet.FirstOrDefault(expression);
            }
            catch (Exception ex)
            {
                // Log exception
                throw new Exception("Could not retrieve entity", ex);
            }
        }

        public virtual T Add(T entity)
        {
            try
            {
                return _dbSet.Add(entity).Entity;
            }
            catch (Exception ex)
            {
                // Log exception
                throw new Exception("Could not add entity", ex);
            }
        }
        
        public virtual void Update(T entity)
        {
            try
            {
                _dbSet.Attach(entity);
                _context.Entry(entity).State = EntityState.Modified;
            }
            catch (Exception ex)
            {
                // Log exception
                throw new Exception("Could not update entity", ex);
            }
        }
        
        public virtual void Delete(int id)
        {
            try
            {
                var entity = _dbSet.Find(id) ?? throw new Exception($"Entity with id {id} not found");
                _dbSet.Remove(entity);
            }
            catch (Exception ex)
            {
                // Log exception
                throw new Exception($"Could not delete entity with id {id}", ex);
            }
        }

        public virtual void DeleteWhere(Expression<Func<T, bool>> predicate)
        {
            try
            {
                var entities = _dbSet.Where(predicate).ToList();
                _dbSet.RemoveRange(entities);
            }
            catch (Exception ex)
            {
                // Log exception
                throw new Exception("Could not delete entities", ex);
            }
        }

        public virtual void Save()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                // Log exception
                throw new Exception("Could not save changes", ex);
            }
        }
    }

}
