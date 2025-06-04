// Application/Services/DataServices/ChallengeNodeService.cs
using AutoMapper;
using ILS_BE.Application.Interfaces;
using ILS_BE.Domain.DTOs;
using ILS_BE.Domain.Interfaces;
using ILS_BE.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using LinqKit;
using ILS_BE.Infrastructure.Repositories;

namespace ILS_BE.Application.Services.DataServices
{
    public class ChallengeNodeService : DataService<ChallengeNode, ChallengeNodeDTO>
    {
        private readonly ChallengeNodeRepository _nodeRepository;
        private readonly IRepository<ChallengeProblem> _problemRepository;
        private readonly IAuthService _authService;
        private readonly IRepository<UserChallengeFinish> _userChallengeFinishRepository;

        public ChallengeNodeService(
            IRepository<ChallengeNode> repository,
            ChallengeNodeRepository nodeRepository,
            IRepository<ChallengeProblem> problemRepository,
            IAuthService authService,
            IRepository<UserChallengeFinish> userChallengeFinishRepository,
            IMapper mapper)
            : base(repository, mapper)
        {
            _nodeRepository = nodeRepository;
            _problemRepository = problemRepository;
            _authService = authService;
            _userChallengeFinishRepository = userChallengeFinishRepository;
        }

        // Pagination with filters: tag, category, state
        public async Task<PaginatedResult<ChallengeNodeDTO>> GetPaginatedAsync(ChallengeNodeFilterDTO filterDTO)
        {
            var userId = _authService.GetUserId();
            var challengeFinishedIds = (await _userChallengeFinishRepository
                .GetWhereAsync(f => f.UserId == userId))
                .Select(f => f.ChallengeId);


            var parent = await _nodeRepository.GetByIdAsync(filterDTO.ParentNodeId)
                ?? throw new Exception("Parent node not found");
            var path = parent.Path + parent.Id + ".";

            var filter = PredicateBuilder.New<ChallengeNode>(n => n.Path == path);
            if (filterDTO.IsProblem)
            {
                filter = filter.And(n => n.IsProblem);
                if (filterDTO.StateIds != null && filterDTO.StateIds.Count > 0)
                    filter = filter.And(n => filterDTO.StateIds.Contains(n.Problem!.ChallengeStateId));
                if (filterDTO.CategoryIds != null && filterDTO.CategoryIds.Count > 0)
                    filter = filter.And(n => filterDTO.CategoryIds.Contains(n.Problem!.CategoryId));
                if (filterDTO.TagIds != null && filterDTO.TagIds.Count > 0)
                    filter = filter.And(n => n.Problem!.Tags.Any(t => filterDTO.TagIds.Contains(t.Id)));
                if (!string.IsNullOrEmpty(filterDTO.SearchTerm))
                    filter = filter.And(n => n.Problem!.Title.Contains(filterDTO.SearchTerm));
                if (!filterDTO.GetSolved)
                    filter = filter.And(n => !challengeFinishedIds.Contains(n.ProblemId!.Value));
            }
            else
            {
                filter = filter.And(n => !n.IsProblem);
                if (!string.IsNullOrEmpty(filterDTO.SearchTerm))
                    filter = filter.And(n => n.Title!.Contains(filterDTO.SearchTerm));
            }

            var paginatedResult = await _nodeRepository
                .GetPaginatedAsync(filterDTO.Page, filterDTO.PageSize, filter);

            var dtos = _mapper.Map<PaginatedResult<ChallengeNodeDTO>>(paginatedResult);
            for ( var i = 0; i < dtos.Items.Count; i++)
            {
                if (dtos.Items[i].IsProblem && dtos.Items[i].Problem != null)
                {
                    dtos.Items[i].Problem!.IsSolved = dtos.Items[i].Problem!.Id != 0 && challengeFinishedIds.Contains(dtos.Items[i].Problem!.Id);
                }
            }

            return dtos;
        }

        // Get nodes with user relative filter (isSolve)
        //public async Task<List<ChallengeNodeDTO>> GetNodesWithUserSolve(int userId)
        //{
        //    var nodes = await _nodeRepository.GetAllAsync();
        //    var finishes = await _userChallengeFinishRepository.GetWhereAsync(f => f.UserId == userId);
        //    var solvedIds = finishes.Select(f => f.ChallengeId).ToHashSet();

        //    var dtos = _mapper.Map<List<ChallengeNodeDTO>>(nodes);
        //    foreach (var dto in dtos)
        //    {
        //        if (dto.Problem != null)
        //            dto.Problem.IsSolved = dto.Problem.Id != 0 && solvedIds.Contains(dto.Problem.Id);
        //    }
        //    return dtos;
        //}

        // Create new node
        public async Task<ChallengeNodeDTO> AddAsync(ChallengeNodeCreateOrUpdateDTO dto)
        {
            var parent = await _nodeRepository.GetByIdAsync(dto.ParentNodeId)
                ?? throw new Exception("Parent node not found");

            var entity = _mapper.Map<ChallengeNode>(dto);
            entity.Path = parent.Path + parent.Id + ".";
            entity = await _nodeRepository.AddAsync(entity);
            await _nodeRepository.SaveAsync();
            return _mapper.Map<ChallengeNodeDTO>(entity);
        }

        // Update node
        public async Task UpdateAsync(ChallengeNodeCreateOrUpdateDTO dto)
        {
            var node = await _nodeRepository.GetByIdAsync(dto.Id)
                ?? throw new Exception("Node not found");
            _mapper.Map(dto, node);
            await _nodeRepository.UpdateAsync(node);
            await _nodeRepository.SaveAsync();
        }

        // Delete node and all subnodes and problems
        public override async Task DeleteAsync(int nodeId)
        {
            var node = await _nodeRepository.GetByIdAsync(nodeId)
                ?? throw new Exception("Node not found");
            var path = node.Path + node.Id + ".";

            // Find all subnodes
            var subNodes = await _nodeRepository.GetWhereAsync(n => n.Path.StartsWith(path));
            foreach (var sub in subNodes)
            {
                if (sub.IsProblem && sub.ProblemId.HasValue)
                    await _problemRepository.DeleteAsync(sub.ProblemId.Value);
            }
            await _problemRepository.SaveAsync();

            // Delete subnodes
            foreach (var sub in subNodes)
                await _nodeRepository.DeleteAsync(sub.Id);

            // Delete this node
            await _nodeRepository.DeleteAsync(nodeId);
            await _nodeRepository.SaveAsync();
        }
    }
}
