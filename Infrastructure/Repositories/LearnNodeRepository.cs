using ILS_BE.Domain.Interfaces;
using ILS_BE.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ILS_BE.Infrastructure.Repositories
{
    public class LearnNodeRepository : Repository<LearnNode>
    {
        public LearnNodeRepository(AppDbContext context) : base(context)
        {
        }

        public override async Task<LearnNode?> GetByIdAsync(int id)
        {
            try
            {
                return await _dbSet
                    .Include(n => n.Lesson)
                    .ThenInclude(l => l.LessonType)
                    .FirstOrDefaultAsync(n => n.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Could not retrieve entity with id {id}", ex);
            }
        }
        public override async Task<List<LearnNode>> GetAllAsync()
        {
            try
            {
                return await _dbSet
                    .Include(n => n.Lesson)
                    .ThenInclude(l => l.LessonType)
                    .OrderBy(n => n.Order)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Could not retrieve entities", ex);
            }
        }
        public override async Task<List<LearnNode>> GetWhereAsync(Expression<Func<LearnNode, bool>> expression)
        {
            try
            {
                return await _dbSet
                    .Include(n => n.Lesson)
                    .ThenInclude(l => l.LessonType)
                    .Where(expression)
                    .OrderBy(n => n.Order)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Could not retrieve entities", ex);
            }
        }
    }
}
