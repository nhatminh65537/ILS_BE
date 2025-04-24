namespace ILS_BE.Domain.DTOs
{
    public class BaseDTO<TKey>
    {
        public TKey Id { get; set; } = default!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
