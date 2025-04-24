using System.Diagnostics.CodeAnalysis;

namespace ILS_BE.Domain.Models
{
    public class LearnNode : NodeEntity<int>
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public bool IsLesson { get; set; } = false;
        public int? LessonId { get; set; }
        public int Order { get; set; } = 0;
        public LearnLesson? Lesson { get; set; }
    }
}
