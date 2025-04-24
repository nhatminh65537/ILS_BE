using ILS_BE.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ILS_BE.Infrastructure.Repositories
{
    public class LessonRepository : GenericRepository<LearnLesson>
    {
        public LessonRepository(DbContext context) : base(context)
        {
        }

        public override async Task<LearnLesson?> GetByIdAsync(int id)
        {
            try
            {
                return await _dbSet
                    .Include(l => l.LessonType)
                    .FirstOrDefaultAsync(l => l.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Could not retrieve entity with id {id}", ex);
            }
        }

        public override async Task<List<LearnLesson>> GetAllAsync()
        {
            try
            {
                return await _dbSet
                    .Include(l => l.LessonType)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Could not retrieve entities", ex);
            }
        }

        public override LearnLesson? GetById(int id)
        {
            try
            {
                return _dbSet
                    .Include(l => l.LessonType)
                    .FirstOrDefault(l => l.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Could not retrieve entity with id {id}", ex);
            }
        }

        public override List<LearnLesson> GetAll()
        {
            try
            {
                return _dbSet
                    .Include(l => l.LessonType)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Could not retrieve entities", ex);
            }
        }
    }
}
