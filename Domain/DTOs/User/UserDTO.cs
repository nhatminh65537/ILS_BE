using System.ComponentModel;
using System.Xml.XPath;

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

    public class UserPublicDTO
    {
        public string DisplayName { get; set; } = null!;
        public string AvatarPath { get; set; } = null!;
        public int Xp { get; set; }
        public int Level { get; set; }
        public int Id { get; set; }
    }
}
