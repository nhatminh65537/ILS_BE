// Application/Services/DataServices/ChallengeProblemService.cs
using AutoMapper;
using ILS_BE.Domain.DTOs;
using ILS_BE.Domain.Interfaces;
using ILS_BE.Domain.Models;

namespace ILS_BE.Application.Services.DataServices
{
    public class ChallengeProblemService : DataService<ChallengeProblem, ChallengeProblemDTO>
    {
        private readonly IRepository<ChallengeTag> _tagRepository;
        private readonly IRepository<ChallengeProblemTag> _problemTagRepository;
        private readonly IRepository<ChallengeFile> _fileRepository;
        private readonly IRepository<ChallengeNode> _nodeRepository;

        public ChallengeProblemService(
            IRepository<ChallengeTag> tagRepository,
            IRepository<ChallengeProblemTag> problemTagRepository,
            IRepository<ChallengeNode> nodeRepository,
            IRepository<ChallengeFile> fileRepository,  
            IRepository<ChallengeProblem> repository,
            IMapper mapper)
            : base(repository, mapper)
        {
            _tagRepository = tagRepository;
            _problemTagRepository = problemTagRepository;
            _fileRepository = fileRepository;
            _nodeRepository = nodeRepository;
        }

        public async Task<ChallengeProblemDTO> AddAsync(ChallengeProblemCreateOrUpdateDTO entityDto)
        {
            var problem = _mapper.Map<ChallengeProblem>(entityDto);
            problem = await _repository.AddAsync(problem);
            await _repository.SaveAsync();

            // Create node for this problem
            if (entityDto.ParentNodeId > 0)
            {
                var parent = await _nodeRepository.GetByIdAsync(entityDto.ParentNodeId)
                    ?? throw new Exception("Parent node not found");

                var problemNode = new ChallengeNode
                {
                    Path = parent.Path + parent.Id + ".",
                    IsProblem = true,
                    ProblemId = problem.Id,
                    Title = null
                };
                await _nodeRepository.AddAsync(problemNode);
                await _nodeRepository.SaveAsync();
            }

            if (entityDto.TagIds != null && entityDto.TagIds.Count > 0)
            {
                await UpdateTagsAsync(problem.Id, entityDto.TagIds);
            }

            return _mapper.Map<ChallengeProblemDTO>(problem);
        }

        public async Task<ChallengeProblemDTO> UpdateAsync(ChallengeProblemCreateOrUpdateDTO entityDto)
        {
            var problem = await _repository.GetByIdAsync(entityDto.Id)
                ?? throw new Exception("Problem not found");

            _mapper.Map(entityDto, problem);
            await _repository.UpdateAsync(problem);
            await _repository.SaveAsync();

            if (entityDto.TagIds != null)
            {
                await UpdateTagsAsync(problem.Id, entityDto.TagIds);
            }

            return _mapper.Map<ChallengeProblemDTO>(problem);
        }

        public async Task UpdateTagsAsync(int problemId, List<int> tagIds)
        {
            await _problemTagRepository.DeleteWhereAsync(pt => pt.ChallengeProblemId == problemId);

            foreach (var tagId in tagIds)
            {
                var tag = await _tagRepository.GetByIdAsync(tagId)
                    ?? throw new Exception($"Tag with ID {tagId} not found");

                await _problemTagRepository.AddAsync(new ChallengeProblemTag
                {
                    ChallengeProblemId = problemId,
                    ChallengeTagId = tagId
                });
            }

            await _problemTagRepository.SaveAsync();
        }

        public async Task UploadFileAsync(ChallengeFileDTO dto)
        {
            if (dto.ChallengeId <= 0)
                throw new ArgumentException("Challenge ID must be greater than zero.");
            var problem = await _repository.GetByIdAsync(dto.ChallengeId)
                ?? throw new Exception("Problem not found");
            var entity = _mapper.Map<ChallengeFile>(dto);
            await _fileRepository.AddAsync(entity);
            await _fileRepository.SaveAsync();
        }
        public async Task DeleteFileAsync(int fileId)
        {
            await _fileRepository.DeleteAsync(fileId);
            await _fileRepository.SaveAsync();
        }
    }
}
