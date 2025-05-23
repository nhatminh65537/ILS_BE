﻿using ILS_BE.Domain.Interfaces;
using ILS_BE.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ILS_BE.Infrastructure.Repositories
{
    public class ModuleRepository : GenericRepository<Module>, IPaginatedRepository<Module>
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

        public async Task<PaginatedResult<Module>> GetPaginatedAsync(int page, int pageSize, Dictionary<string, object> filters)
        {
            try
            {
                IQueryable<Module> query = _dbSet
                    .Include(m => m.Category)
                    .Include(m => m.LifecycleState)
                    .Include(m => m.Tags);

                var totalItems = await query.CountAsync();

                var tagIds = filters["tagId"] as List<int>;
                var categoryIds = filters["categoryId"] as List<int>;
                var lifecycleStateIds = filters["lifecycleStateId"] as List<int>;
                var searchTerm = filters["searchTerm"] as string;

                var items = await query
                    .Where(m => categoryIds!.Contains(m.CategoryId) || categoryIds.Count == 0)
                    .Where(m => tagIds!.Intersect(m.Tags.Select(t => t.Id)).Count() > 0 || tagIds!.Count == 0)
                    .Where(m => lifecycleStateIds!.Contains(m.LifecycleStateId) || lifecycleStateIds.Count == 0)
                    .Where(m => m.Title.ToLower().Contains(searchTerm!.ToLower()) || string.IsNullOrEmpty(searchTerm))
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
                return new PaginatedResult<Module>
                {
                    CurrentPage = page,
                    TotalItems = totalItems,
                    TotalPages = (int)Math.Ceiling((double)totalItems / pageSize),
                    Items = items
                };
            }
            catch (Exception ex)
            {
                // Log exception
                throw new Exception("Could not retrieve paginated entities", ex);
            }
        }
    }
}
