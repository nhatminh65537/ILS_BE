using AutoMapper;
using ILS_BE.Application.Interfaces;
using ILS_BE.Domain.DTOs;
using ILS_BE.Domain.Interfaces;
using ILS_BE.Domain.Models;

namespace ILS_BE.Application.Services
{
    public class ContentItemService : DataService<LearnNode, LearnNodeDTO>, IContentItemService, IDataService<LearnNodeDTO>
    {
        private readonly IContentItemRepository _contentItemRepository;
        private readonly IGenericRepository<LearnModule> _moduleRepository;
        private readonly IGenericRepository<LearnLesson> _lessonRepository;

        public ContentItemService(
            IContentItemRepository contentItemRepository,
            IGenericRepository<LearnModule> moduleRepository,
            IGenericRepository<LearnLesson> lessonRepository,
            IMapper mapper)
            : base(contentItemRepository, mapper)
        {
            _contentItemRepository = contentItemRepository;
            _moduleRepository = moduleRepository;
            _lessonRepository = lessonRepository;
        }

        public async Task<ContentItemTreeDTO> GetModuleTreeByIdAsync(int id)
        {
            var listNodes = _contentItemRepository.GetContentItemsInModule(id);
            var root = listNodes.First();
            
            return await DeepMapping(root, listNodes);
        }
        private async Task<ContentItemTreeDTO> DeepMapping(LearnNode root, List<LearnNode> listNodes)
        {
            var contentItemDTO = _mapper.Map<LearnNodeDTO>(root);

            var contentItemTreeDTO = new ContentItemTreeDTO
            {
                Item = contentItemDTO,
                Children = new List<ContentItemTreeDTO>()
            };

            string nextPath = root.Path + root.Id.ToString() + ".";
            var childs = listNodes.Where(ci => ci.Path == nextPath);
            foreach (var child in childs)
            {
                contentItemTreeDTO.Children.Add(await DeepMapping(child, listNodes));
            }
            return contentItemTreeDTO;
        }

        public async Task UpdateModuleTreeAsync(ContentItemTreeDTO moduleDetailDTO)
        {
            await UpdateContentItem(moduleDetailDTO, ".");
            await _contentItemRepository.SaveAsync();
        }
        private async Task UpdateContentItem(ContentItemTreeDTO contentItemTree, string path)
        {
            var contentItemModel = _mapper.Map<LearnNode>(contentItemTree.Item);
            contentItemModel.Path = path;
            await _contentItemRepository.UpdateAsync(contentItemModel);

            string nextPath = path + contentItemModel.Id.ToString() + ".";
            foreach (var child in contentItemTree.Children)
            {
                await UpdateContentItem(child, nextPath);
            }
        }

        public async Task<LearnNodeDTO> AddModuleAsync(ModuleDTO moduleDto)
        {
            var contentItem = new LearnNode
            {
                IsModule = true,
                Path = "."
            };
            contentItem = await _contentItemRepository.AddAsync(contentItem);
            await _contentItemRepository.SaveAsync();
            
            var module = _mapper.Map<LearnModule>(moduleDto);
            module = await _moduleRepository.AddAsync(module);
            module.NodeId = contentItem.Id;
            
            await _moduleRepository.SaveAsync();
            contentItem.Module = module;
            contentItem.ModuleId = module.Id;
            await _moduleRepository.SaveAsync();

            return _mapper.Map<LearnNodeDTO>(contentItem);
        }

        public override async Task<LearnNodeDTO> AddAsync(LearnNodeDTO entityDto)
        {
            var parentId = entityDto.ParentId 
                ?? throw new Exception("ParentId of ContentItem is null");

            var parent = await _contentItemRepository.GetByIdAsync((int)parentId)
                ?? throw new Exception("Parent of ContentItem not found");

            var entity = _mapper.Map<LearnNode>(entityDto);
            entity.Path = parent.Path + parent.Id.ToString() + ".";
            entity = await _contentItemRepository.AddAsync(entity);
            await _contentItemRepository.SaveAsync();
            entityDto = _mapper.Map<LearnNodeDTO>(entity);
            return entityDto;
      
        }

        public override async Task DeleteAsync(int id)
        {
            await _contentItemRepository.DeleteWhereAsync(c => c.Path.StartsWith(id.ToString() + "."));
        }

        public Task<LearnNodeDTO> AddLessonAsync(LessonDTO lessonDTO)
        {
            throw new NotImplementedException();
        }
    }
}
