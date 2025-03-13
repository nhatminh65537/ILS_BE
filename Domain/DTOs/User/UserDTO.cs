namespace ILS_BE.Domain.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public bool EmailVerified { get; set; }
        public string? PhoneNumber { get; set; }
        public bool RequirePasswordReset { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
