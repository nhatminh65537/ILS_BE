namespace ILS_BE.Domain.Models
{
    public class LearnLesson : BaseEntity<int>
    {
        public string Title { get; set; } = null!;
        public int NodeId { get; set; }
        public int TypeId { get; set; }
        public string Content { get; set; } = String.Empty;
        public int Xp { get; set; } = 0;
        public int Duration { get; set; } = 0;

        public LearnLessonType LessonType { get; set; } = null!;
        public LearnNode Node { get; set; } = null!;
    }
}
