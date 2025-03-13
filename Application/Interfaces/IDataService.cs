using System.Collections.Generic;
using System.Threading.Tasks;

namespace ILS_BE.Application.Interfaces
{
    public interface IDataService<TDTO> where TDTO : class
    {
        Task<List<TDTO>> GetAllAsync();
        Task<TDTO?> GetByIdAsync(int id);
        Task<TDTO> AddAsync(TDTO entity);
        Task UpdateAsync(TDTO entity);
        Task DeleteAsync(int id);
        List<TDTO> GetAll();
        TDTO? GetById(int id);
        TDTO Add(TDTO entity);
        void Delete(int id);
        void Update(TDTO entity);
    }
}
