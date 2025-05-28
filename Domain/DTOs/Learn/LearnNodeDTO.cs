using ILS_BE.Domain.Models;

namespace ILS_BE.Domain.DTOs
{
    public class LearnNodeDTO : BaseDTO<int>
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public LearnLessonNodeDTO? Lesson { get; set; }
        public int Order { get; set; } = 0;
    }

    public class LearnNodeCreateOrUpdateDTO : BaseDTO<int>
    {
        public int ParentNodeId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int Order { get; set; } = 0;
    }
}
