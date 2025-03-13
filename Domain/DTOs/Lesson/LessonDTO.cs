namespace ILS_BE.Domain.DTOs
{
    public class LessonDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public int ContentItemId { get; set; }
        public LessonTypeDTO LessonType { get; set; } = null!;
        public int Xp { get; set; }
        public int Duration { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
