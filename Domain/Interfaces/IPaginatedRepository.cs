namespace ILS_BE.Domain.Interfaces
{
    public interface IPaginatedRepository<TModel> : IRepository<TModel>
        where TModel : class
    {
        public Task<PaginatedResult<TModel>> GetPaginatedAsync(int page, int pageSize, Dictionary<string, object> filters);
    }

    public class PaginatedResult<T>
    {
        public List<T> Items { get; set; } = new List<T>();
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
    }
}
