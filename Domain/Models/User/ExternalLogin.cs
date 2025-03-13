namespace ILS_BE.Domain.Models
{
    public class ExternalLogin
    {
        public int UserId { get; set; }
        public string Provider { get; set; } = null!;
        public string ProviderUserId { get; set; } = null!;
        public string ProviderData { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
