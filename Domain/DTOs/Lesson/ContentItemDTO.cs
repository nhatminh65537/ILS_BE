using ILS_BE.Domain.Models;

namespace ILS_BE.Domain.DTOs
{
    public class ContentItemDTO
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public ModuleDTO? Module { get; set; }
        public LessonDTO? Lesson { get; set; }
        public int Order { get; set; } = 0;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    }
}
