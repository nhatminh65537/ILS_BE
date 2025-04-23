namespace ILS_BE.Domain.Models
{
    public class Module
    {
        public int Id { get; set; }
        public int ContentItemId { get; set; }
        public string Title { get; set; } = null!;
        public string? ImagePath { get; set; }
        public int? CreatedBy { get; set; }
        public string? Description { get; set; }
        public int CategoryId { get; set; }
        public int LifecycleStateId { get; set; }
        public int Xp { get; set; } = 0;
        public int Duration { get; set; } = 0;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public Category Category { get; set; } = null!;
        public LifecycleState LifecycleState { get; set; } = null!;
        public List<Tag> Tags { get; set; } = new List<Tag>();
    }
}
