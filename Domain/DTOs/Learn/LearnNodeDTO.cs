using ILS_BE.Domain.Models;

namespace ILS_BE.Domain.DTOs
{
    public class LearnNodeDTO : BaseDTO<int>
    {
        public int? ParentId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public LessonDTO? Lesson { get; set; }
        public int Order { get; set; } = 0;
    }
}
