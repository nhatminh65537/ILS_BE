using ILS_BE.Domain.Models;

namespace ILS_BE.Domain.DTOs
{
    public class LearnModuleDTO : BaseDTO<int>
    {
        public string Title { get; set; } = null!;
        public string? ImagePath { get; set; }
        public int? CreatedBy { get; set; }
        public string? Description { get; set; }
        public LearnCategoryDTO Category { get; set; } = null!;
        public LearnLifecycleStateDTO LifecycleState { get; set; } = null!;
        public List<LearnTagDTO> Tags { get; set; } = null!;
        public LearnNodeDTO Node { get; set; } = null!;
        public int Xp { get; set; } = 0;
        public int Duration { get; set; } = 0;
    }

    public class LearnModuleCreateOrUpdateDTO : BaseDTO<int>
    {
        public string Title { get; set; } = null!;
        public string? ImagePath { get; set; }
        public int? CreatedBy { get; set; }
        public string Description { get; set; } = String.Empty;
        public int CategoryId { get; set; }
        public List<int> TagIds { get; set; } = null!;
        public int LifecycleStateId { get; set; }
    }
}
