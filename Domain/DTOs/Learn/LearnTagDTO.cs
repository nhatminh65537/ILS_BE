namespace ILS_BE.Domain.DTOs
{
    public class LearnTagDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = String.Empty;
    }
}
