using AutoMapper;
using ILS_BE.Application.Interfaces;
using ILS_BE.Domain.Interfaces;

namespace ILS_BE.Application.Services
{
    public class    PaginatedDataService<TModel, TDTO>: DataService<TModel, TDTO>, IPaginatedDataService<TDTO>
        where TDTO : class
        where TModel : class
    {
        new IPaginatedRepository<TModel> _repository;

        public PaginatedDataService(IPaginatedRepository<TModel> repository, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
        }

        public async Task<PaginatedResult<TDTO>> GetPaginatedAsync(int page, int pageSize, Dictionary<string, object> filters)
        {
            var paginatedResult = await _repository.GetPaginatedAsync(page, pageSize, filters);
            var mappedResult = _mapper.Map<PaginatedResult<TDTO>>(paginatedResult);
            return mappedResult;
        }
    }
}
