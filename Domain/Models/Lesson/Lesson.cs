namespace ILS_BE.Domain.Models
{
    public class Lesson
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public int ContentItemId { get; set; }
        public int TypeId { get; set; }
        public string Content { get; set; } = null!;
        public int Xp { get; set; } = 0;
        public int Duration { get; set; } = 0;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public LessonType LessonType { get; set; } = null!;
    }
}
