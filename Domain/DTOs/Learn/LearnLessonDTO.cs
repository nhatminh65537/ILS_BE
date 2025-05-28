namespace ILS_BE.Domain.DTOs
{
    public class LearnLessonNodeDTO : BaseDTO<int>
    {
        public string Title { get; set; } = null!;
        public LearnLessonTypeDTO LessonType { get; set; } = null!;
        public int Xp { get; set; }
        public int Duration { get; set; }
    }

    public class LearnLessonCreateOrUpdateDTO : BaseDTO<int>
    {
        public int ParentNodeId { get; set; }
        public string Title { get; set; } = null!;
        public int TypeId { get; set; }
        public int Xp { get; set; } = 0;
        public int Duration { get; set; } = 0;
        public string Content { get; set; } = String.Empty;
    }

    public class LearnLessonDTO : BaseDTO<int>
    {
        public string Title { get; set; } = null!;
        public string Content { get; set; } = String.Empty;
        public LearnLessonTypeDTO LessonType { get; set; } = null!;
        public int Xp { get; set; }
        public int Duration { get; set; }
    }
}
