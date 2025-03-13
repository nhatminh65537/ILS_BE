using ILS_BE.Domain.Models;

namespace ILS_BE.Domain.DTOs
{
    public class ModuleDTO
    {
        public int Id { get; set; }
        public int ContentItemId { get; set; }
        public string Title { get; set; } = null!;
        public int CategoryId { get; set; }
        public int LifecycleStateId { get; set; }
        public string? ImagePath { get; set; }
        public int? CreatedBy { get; set; }
        public string? Description { get; set; }
        public CategoryDTO? Category { get; set; }
        public LifecycleStateDTO? LifecycleState { get; set; }
        public List<TagDTO> Tags { get; set; } = new List<TagDTO>();
        public int Xp { get; set; } = 0;
        public int Duration { get; set; } = 0;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
