using ILS_BE.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ILS_BE.Infrastructure.Repositories
{
    public class LearnLessonRepository : Repository<LearnLesson>
    {
        public LearnLessonRepository(AppDbContext context) : base(context)
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
        public override async Task<List<LearnLesson>> GetWhereAsync(Expression<Func<LearnLesson, bool>> expression)
        {
            try
            {
                return await _dbSet
                    .Include(l => l.LessonType)
                    .Where(expression)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Could not retrieve entities", ex);
            }
        }
    }
}
