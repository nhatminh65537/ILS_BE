using AutoMapper;
using ILS_BE.Domain.DTOs;
using ILS_BE.Domain.Interfaces;
using ILS_BE.Domain.Models;

namespace ILS_BE.Application.Services.DataServices
{
    public class LearnLessonService : DataService<LearnLesson, LearnLessonDTO>
    {
        private readonly IRepository<LearnNode> _nodeRepository;
        private readonly IRepository<LearnModule> _moduleRepository;
        public LearnLessonService(
            IRepository<LearnNode> nodeRepository,
            IRepository<LearnModule> moduleRepository,
            IRepository<LearnLesson> repository,
            IMapper mapper) : base(repository, mapper)
        {
            _moduleRepository = moduleRepository;
            _nodeRepository = nodeRepository;
        }

        public async Task<LearnLessonNodeDTO> AddAsync(LearnLessonCreateOrUpdateDTO entityDto)
        {
            var lesson = _mapper.Map<LearnLesson>(entityDto);
            lesson = await _repository.AddAsync(lesson);
            await _repository.SaveAsync();

            var parentId = entityDto.ParentNodeId;

            var parent = await _nodeRepository.GetByIdAsync(parentId)
                ?? throw new Exception("Parent of ContentItem not found");

            var lessonNode = new LearnNode
            {
                Path = parent.Path + parentId.ToString() + ".",
                IsLesson = true,
                LessonId = lesson.Id
            };

            var nodeSibling = await _nodeRepository.GetWhereAsync(c => c.Path.StartsWith(parent.Path));

            if (lessonNode.Order == 0)
            {
                lessonNode.Order = nodeSibling.LastOrDefault()?.Order + 1 ?? 1;
            }

            lessonNode = await _nodeRepository.AddAsync(lessonNode);
            await _nodeRepository.SaveAsync();

            var rootId = int.Parse(lessonNode.Path.Split('.')[1]);
            var module = await _moduleRepository.GetFirstWhereAsync(m => m.NodeId == rootId)
                ?? throw new Exception("Module not found");

            module.Xp += entityDto.Xp;
            module.Duration += entityDto.Duration;
            await _moduleRepository.SaveAsync();

            return _mapper.Map<LearnLessonNodeDTO>(lesson);
        }

        public async Task<LearnLessonNodeDTO> UpdateAsync(LearnLessonCreateOrUpdateDTO entityDto)
        {
            var lesson = await _repository.GetByIdAsync(entityDto.Id)
                ?? throw new Exception("Lesson not found");
            var xp = -lesson.Xp + entityDto.Xp;
            var duration = -lesson.Duration + entityDto.Duration;
            _mapper.Map(entityDto, lesson);
            await _repository.UpdateAsync(lesson);
            await _repository.SaveAsync();

            var lessonNode = await _nodeRepository.GetFirstWhereAsync(n => n.LessonId == lesson.Id)
                ?? throw new Exception("Lesson node not found");
            var rootId = int.Parse(lessonNode.Path.Split('.')[1]);
            var module = await _moduleRepository.GetFirstWhereAsync(m => m.NodeId == rootId)
                ?? throw new Exception("Module not found");
            module.Xp += xp;
            module.Duration += duration;
            await _moduleRepository.SaveAsync();

            return _mapper.Map<LearnLessonNodeDTO>(lesson);
        }
    }
}
