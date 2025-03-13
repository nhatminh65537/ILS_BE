namespace ILS_BE.Domain.Models
{
    public class UserFinishedLesson
    {
        public int UserId { get; set; }
        public int LessonId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}

