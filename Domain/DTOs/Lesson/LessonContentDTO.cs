namespace ILS_BE.Domain.DTOs
{
    public class LessonContentDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Content { get; set; }
    }
}
