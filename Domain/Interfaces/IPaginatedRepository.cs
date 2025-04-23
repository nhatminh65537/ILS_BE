namespace ILS_BE.Domain.Interfaces
{
    public interface IPaginatedRepository<TModel> : IGenericRepository<TModel>
        where TModel : class
    {
        public Task<PaginatedResult<TModel>> GetPaginatedAsync(int page, int pageSize, Dictionary<string, object> filters);
    }

    public class PaginatedResult<TModel>
    {
        public List<TModel> Items { get; set; } = new List<TModel>();
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
    }
}
