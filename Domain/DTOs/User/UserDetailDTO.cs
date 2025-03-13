using ILS_BE.Domain.Models;
using System.ComponentModel;

namespace ILS_BE.Domain.DTOs
{
    public class UserDetailDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public bool EmailVerified { get; set; } = false;
        public string? PhoneNumber { get; set; }
        public bool RequirePasswordReset { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public UserProfileDTO? Profile { get; set; }
        public List<PermissionDTO>? Permissions { get; set; }
        public List<RoleDTO>? Roles { get; set; }
    }
}
