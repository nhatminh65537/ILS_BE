namespace ILS_BE.Domain.Models
{
    public class LearnCategory
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = String.Empty;
    }
}
