using ILS_BE.Domain.DTOs;
using ILS_BE.Domain.Interfaces;

namespace ILS_BE.Application.Interfaces
{
    public interface IPaginatedDataService<TDTO> : IDataService<TDTO>
         where TDTO : class
    {
        Task<PaginatedResult<TDTO>> GetPaginatedAsync(int page, int pageSize, Dictionary<string, object> filters);
    }
}
