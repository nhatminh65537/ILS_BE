using System.Diagnostics.CodeAnalysis;

namespace ILS_BE.Domain.Models
{
    public class ContentItem
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Path { get; set; }
        public bool IsModule { get; set; } = false;
        public int? ModuleId { get; set; }
        public bool IsLesson { get; set; } = false;
        public int? LessonId { get; set; }
        public int Order { get; set; } = 0;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public Lesson? Lesson { get; set; }
        public Module? Module { get; set; }
    }
}
