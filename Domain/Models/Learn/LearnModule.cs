namespace ILS_BE.Domain.Models
{
    public class LearnModule : BaseEntity<int>
    {
        public int NodeId { get; set; }
        public string Title { get; set; } = null!;
        public string? ImagePath { get; set; }
        public int? CreatedBy { get; set; }
        public string Description { get; set; } = String.Empty;
        public int CategoryId { get; set; }
        public int LifecycleStateId { get; set; }
        public int Xp { get; set; } = 0;
        public int Duration { get; set; } = 0;

        public LearnCategory Category { get; set; } = null!;
        public LearnLifecycleState LifecycleState { get; set; } = null!;
        public List<LearnTag> Tags { get; set; } = null!;
        public LearnNode Node { get; set; } = null!;
    }
}
