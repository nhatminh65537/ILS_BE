namespace ILS_BE.Domain.DTOs
{
    public class ContentItemTreeDTO
    {
        public LearnNodeDTO Item { get; set; } = null!;
        public List<ContentItemTreeDTO> Children { get; set; } = new List<ContentItemTreeDTO>();
    }
}
