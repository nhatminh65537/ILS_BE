namespace ILS_BE.Domain.Models
{
    public class LearnModuleTag
    {
        public int ModuleId { get; set; }
        public int TagId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
