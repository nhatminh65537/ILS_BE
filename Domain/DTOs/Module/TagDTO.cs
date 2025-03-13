namespace ILS_BE.Domain.DTOs
{
    public class TagDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
    }
}
