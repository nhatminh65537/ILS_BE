using AutoMapper;
using ILS_BE.Application.Interfaces;
using ILS_BE.Domain.DTOs;
using ILS_BE.Domain.Interfaces;
using ILS_BE.Domain.Models;

namespace ILS_BE.Application.Services
{
    public class LearnNodeService : DataService<LearnNode, LearnNodeDTO>, INodeService<LearnNodeDTO>
    {
        private readonly IRepository<LearnNode> _nodeRepository;
        private readonly IRepository<LearnLesson> _lessonRepository;

        public LearnNodeService(
            IRepository<LearnNode> nodeRepository,
            IRepository<LearnLesson> lessonRepository,
            IMapper mapper)
            : base(nodeRepository, mapper)
        {
            _lessonRepository = lessonRepository;
            _nodeRepository = nodeRepository;
        }

        public async Task<TreeDTO<LearnNodeDTO>> GetTreeByIdAsync(int rootId)
        {
            var root = await _nodeRepository.GetByIdAsync(rootId)
                ?? throw new Exception("Node not found");
            var path = "." + rootId.ToString() + ".";
            var listNodes = await _nodeRepository.GetWhereAsync(n => n.Path.StartsWith(path));

            return await DeepMapping(root, listNodes);
        }
        private async Task<TreeDTO<LearnNodeDTO>> DeepMapping(LearnNode root, List<LearnNode> listNodes)
        {
            var learnNodeDTO = _mapper.Map<LearnNodeDTO>(root);

            var learnTreeDTO = new TreeDTO<LearnNodeDTO>
            {
                Item = learnNodeDTO,
                Children = new List<TreeDTO<LearnNodeDTO>>()
            };

            string nextPath = root.Path + root.Id.ToString() + ".";
            var childs = listNodes.Where(ci => ci.Path == nextPath);
            foreach (var child in childs)
            {
                learnTreeDTO.Children.Add(await DeepMapping(child, listNodes));
            }
            return learnTreeDTO;
        }

        public async Task UpdateTreeAsync(TreeDTO<LearnNodeDTO> learnTreeDto)
        {
            await UpdateNode(learnTreeDto, ".");
            await _nodeRepository.SaveAsync();
        }
        private async Task UpdateNode(TreeDTO<LearnNodeDTO> learnTree, string path)
        {
            var nodeEntity = _mapper.Map<LearnNode>(learnTree.Item);
            nodeEntity.Path = path;
            await _nodeRepository.UpdateAsync(nodeEntity);

            string nextPath = path + nodeEntity.Id.ToString() + ".";
            foreach (var child in learnTree.Children)
            {
                await UpdateNode(child, nextPath);
            }
        }


        public async Task<LearnNodeDTO> AddAsync(LearnNodeCreateOrUpdateDTO entityDto)
        {
            var parentId = entityDto.ParentNodeId;

            var parent = await _nodeRepository.GetByIdAsync(parentId)
                ?? throw new Exception("Parent of ContentItem not found");

            var entity = _mapper.Map<LearnNode>(entityDto);

            var nodeSibling = await _nodeRepository.GetWhereAsync(c => c.Path.StartsWith(parent.Path));
            
            if (entity.Order == 0)
            {
                entity.Order = nodeSibling.LastOrDefault()?.Order + 1 ?? 1;
            }
            entity.Path = parent.Path + parent.Id.ToString() + ".";
            entity = await _nodeRepository.AddAsync(entity);
            await _nodeRepository.SaveAsync();
            return _mapper.Map<LearnNodeDTO>(entity);

        }

        public override async Task DeleteAsync(int nodeId)
        {
            var node = await _nodeRepository.GetByIdAsync(nodeId)
                ?? throw new Exception("Node not found");
            var path = node.Path + node.Id.ToString() + ".";

            var lessonChildren = await _nodeRepository.GetWhereAsync(c => c.Path.StartsWith(path) && c.IsLesson);
            for (var i = 0; i < lessonChildren.Count; i++)
            {
                await _lessonRepository.DeleteAsync(lessonChildren[i].Lesson!.Id);
            }
            await _lessonRepository.SaveAsync();

            await _nodeRepository.DeleteAsync(nodeId);
            await _nodeRepository.DeleteWhereAsync(c => c.Path.StartsWith(path));
            await _nodeRepository.SaveAsync();
        }

        public async Task UpdateAsync(LearnNodeCreateOrUpdateDTO Dto)
        {
            var node = await _nodeRepository.GetByIdAsync(Dto.Id)
                ?? throw new Exception("Node not found");
            _mapper.Map(Dto, node);
            await _nodeRepository.UpdateAsync(node);
            await _nodeRepository.SaveAsync();
        }
    }
}
