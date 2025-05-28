using AutoMapper;
using ILS_BE.Application.Interfaces;
using ILS_BE.Domain.DTOs;
using ILS_BE.Domain.Interfaces;
using ILS_BE.Domain.Models;
using ILS_BE.Infrastructure.Repositories;

namespace ILS_BE.Application.Services.DataServices
{
    public class LearnModuleService : PaginatedDataService<LearnModule, LearnModuleDTO>
    {
        private readonly IRepository<LearnNode> _nodeRepository;
        private readonly LearnNodeService _nodeService;
        private readonly IRepository<LearnTag> _tagRepository;
        private readonly IRepository<LearnModuleTag> _moduleTagRepository;
        public LearnModuleService(
            IRepository<LearnNode> nodeRepository,
            LearnNodeService nodeService,
            IRepository<LearnTag> tagRepository,
            IRepository<LearnModuleTag> moduleTagRepository,
            LearnModuleRepository repository,
            IMapper mapper) : base(repository, mapper)
        {
            _nodeRepository = nodeRepository;
            _nodeService = nodeService;
            _tagRepository = tagRepository;
            _moduleTagRepository = moduleTagRepository;
        }

        public async Task<LearnModuleDTO> AddAsync(LearnModuleCreateOrUpdateDTO entityDto)
        {
            var root = new LearnNode
            {
                Path = "."
            };
            root = await _nodeRepository.AddAsync(root);
            await _nodeRepository.SaveAsync();

            var module = _mapper.Map<LearnModule>(entityDto);
            module = await _repository.AddAsync(module);
            module.NodeId = root.Id;

            await _repository.SaveAsync();

            return _mapper.Map<LearnModuleDTO>(module);
        }

        public async Task<LearnModuleDTO> UpdateAsync(LearnModuleCreateOrUpdateDTO entityDto)
        {
            var module = await _repository.GetByIdAsync(entityDto.Id)
                ?? throw new Exception("Module not found");
            _mapper.Map(entityDto, module);
            await _repository.UpdateAsync(module);
            await _repository.SaveAsync();

            await UpdateTagsAsync(module.Id, entityDto.TagIds);

            return _mapper.Map<LearnModuleDTO>(module);
        }

        public async Task UpdateTagsAsync(int moduleId, List<int> tagIds)
        {
            var module = await _repository.GetByIdAsync(moduleId)
                ?? throw new Exception("Module not found");
            module.Tags.Clear();
            await _moduleTagRepository.DeleteWhereAsync(m => m.ModuleId == moduleId);
            foreach (var tagId in tagIds)
            {
                var tag = await _tagRepository.GetByIdAsync(tagId)
                    ?? throw new Exception("Tag not found");
                var moduleTag = await _moduleTagRepository.AddAsync(new LearnModuleTag
                {
                    ModuleId = moduleId,
                    TagId = tagId
                });
                await _moduleTagRepository.SaveAsync();
            }
        }

        public override async Task DeleteAsync(int id)
        {
            var module = await _repository.GetByIdAsync(id)
                ?? throw new Exception("Module not found");

            await _nodeService.DeleteAsync(module.NodeId);            
        }
    }
}
