using AutoMapper;
using ILS_BE.Application.Interfaces;
using ILS_BE.Domain.DTOs;
using ILS_BE.Domain.Interfaces;
using ILS_BE.Domain.Models;

namespace ILS_BE.Application.Services
{
    public class ContentItemService : IContentItemService
    {
        private readonly IContentItemRepository _contentItemRepository;
        private readonly IGenericRepository<Module> _moduleRepository;
        private readonly IGenericRepository<Lesson> _lessonRepository;
        private readonly IMapper _mapper;

        public ContentItemService(
            IContentItemRepository contentItemRepository,
            IGenericRepository<Module> moduleRepository,
            IGenericRepository<Lesson> lessonRepository,
            IMapper mapper)
        {
            _contentItemRepository = contentItemRepository;
            _moduleRepository = moduleRepository;
            _lessonRepository = lessonRepository;
            _mapper = mapper;
        }

        public async Task<ContentItemTreeDTO> GetModuleTreeByIdAsync(int id)
        {
            var listNodes = _contentItemRepository.GetContentItemsInModule(id);
            var root = listNodes.First();
            
            return await DeepMapping(root, listNodes);
        }
        private async Task<ContentItemTreeDTO> DeepMapping(ContentItem root, List<ContentItem> listNodes)
        {
            var contentItemDTO = _mapper.Map<ContentItemDTO>(root);

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
            var contentItemModel = _mapper.Map<ContentItem>(contentItemTree.Item);
            contentItemModel.Path = path;
            await _contentItemRepository.UpdateAsync(contentItemModel);

            string nextPath = path + contentItemModel.Id.ToString() + ".";
            foreach (var child in contentItemTree.Children)
            {
                await UpdateContentItem(child, nextPath);
            }
        }
    }
}
