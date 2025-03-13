namespace ILS_BE.Domain.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public bool EmailVerified { get; set; } = false;
        public string? PhoneNumber { get; set; }
        public string PasswordHash { get; set; } = null!;
        public bool RequirePasswordReset { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public UserProfile Profile { get; set; } = null!;
        public List<Role> Roles { get; set; } = new List<Role>();
        public List<Permission> Permissions { get; set; } = new List<Permission>();
    }
}
