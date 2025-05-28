using ILS_BE.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using ILS_BE.Application.Interfaces;
using AutoMapper;
using ILS_BE.Domain.MappingProfiles;

namespace ILS_BE.Application.Services
{
    public class DataService<TModel, TDTO> : IDataService<TDTO>
        where TDTO : class
        where TModel : class
    {
        protected readonly IRepository<TModel> _repository;
        protected readonly IMapper _mapper;

        public DataService(IRepository<TModel> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public virtual async Task<List<TDTO>> GetAllAsync()
        { 
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<List<TDTO>>(entities);
        }

        public virtual async Task<TDTO?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return _mapper.Map<TDTO>(entity);
        }


        public virtual async Task<TDTO> AddAsync(TDTO entityDto)
        {
            var entity = await _repository.AddAsync(_mapper.Map<TModel>(entityDto));
            await _repository.SaveAsync();
            return _mapper.Map<TDTO>(entity);
        }

        public virtual async Task UpdateAsync(TDTO entityDto)
        {
            var id = typeof(TDTO).GetProperty("Id")?.GetValue(entityDto)
                ?? throw new Exception($"DataService: {nameof(TDTO)} does not have an Id property");

            var entity = await _repository.GetByIdAsync((int)id)
                ?? throw new Exception("DataService: Entity has ID not found");
            
            _mapper.Map(entityDto, entity);
            await _repository.UpdateAsync(entity);
            await _repository.SaveAsync();
        }

        public virtual async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
            await _repository.SaveAsync();
        }

    }
}
