namespace ILS_BE.Domain.DTOs
{
    public class UserProfileDTO
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string DisplayName { get; set; } = null!;
        public int Xp { get; set; } = 0;
        public int Level { get; set; } = 1;
        public string? AvatarPath { get; set; }
        public string? Bio { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}