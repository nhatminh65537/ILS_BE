using System.Linq.Expressions;

namespace ILS_BE.Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<List<T>> GetWhereAsync(Expression<Func<T, bool>>  expression);
        Task<T?> GetByIdAsync(int id);
        Task<T?> GetFirstWhereAsync(Expression<Func<T, bool>> expression);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
        Task DeleteWhereAsync(Expression<Func<T, bool>> expression);
        Task SaveAsync();

        List<T> GetAll();
        List<T> GetWhere(Expression<Func<T, bool>> expression);
        T? GetById(int id);
        T? GetFirstWhere(Expression<Func<T, bool>> expression);
        T Add(T entity);
        void Update(T entity);
        void Delete(int id);
        void DeleteWhere(Expression<Func<T, bool>> expression);
        void Save();
    }
}