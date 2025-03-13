namespace ILS_BE.Domain.Models
{
    public class ModuleTag
    {
        public int ModuleId { get; set; }
        public int TagId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
