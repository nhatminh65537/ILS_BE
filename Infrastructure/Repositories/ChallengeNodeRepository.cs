// Infrastructure/Repositories/ChallengeNodeRepository.cs
using ILS_BE.Domain.Interfaces;
using ILS_BE.Domain.Models;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ILS_BE.Infrastructure.Repositories
{
    public class ChallengeNodeRepository : Repository<ChallengeNode>, IPaginatedRepository<ChallengeNode>
    {
        public ChallengeNodeRepository(AppDbContext context) : base(context)
        {
        }

        public IQueryable<ChallengeNode> QueryWithIncludes()
        {
            return _dbSet
                .Include(n => n.Problem)
                    .ThenInclude(p => p!.Category)
                .Include(n => n.Problem)
                    .ThenInclude(p => p!.ChallengeState)
                .Include(n => n.Problem)
                    .ThenInclude(p => p!.Files)
                .Include(n => n.Problem)
                    .ThenInclude(p => p!.Tags);
        }
        
        public override async Task<ChallengeNode?> GetByIdAsync(int id)
        {
            try
            {
                return await QueryWithIncludes()
                    .FirstOrDefaultAsync(n => n.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Could not retrieve entity with id {id}", ex);
            }
        }
        
        public override async Task<List<ChallengeNode>> GetAllAsync()
        {
            try
            {
                return await QueryWithIncludes().ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Could not retrieve entities", ex);
            }
        }
        
        public override async Task<List<ChallengeNode>> GetWhereAsync(Expression<Func<ChallengeNode, bool>> expression)
        {
            try
            {
                return await QueryWithIncludes()
                    .Where(expression)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Could not retrieve entities", ex);
            }
        }

        public Task<PaginatedResult<ChallengeNode>> GetPaginatedAsync(int page, int pageSize, Dictionary<string, object> filters)
        {
            throw new NotImplementedException();
        }

        public async Task<PaginatedResult<ChallengeNode>> GetPaginatedAsync(int page, int pageSize, Expression<Func<ChallengeNode, bool>> filters)
        {
            try
            {
                var query = QueryWithIncludes();
                var total = query.Count();
                var items = await query.AsExpandable().Where(filters).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

                return new PaginatedResult<ChallengeNode>
                {
                    CurrentPage = page,
                    TotalItems = total,
                    TotalPages = (int)Math.Ceiling((double)total / pageSize),
                    Items = items
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Could not retrieve paginated entities", ex);
            }
        }
    }
}
