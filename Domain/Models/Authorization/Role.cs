namespace ILS_BE.Domain.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public bool Changeable { get; set; } = true;
    }
}
