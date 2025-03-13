using System.ComponentModel.DataAnnotations;

namespace ILS_BE.Domain.DTOs
{

    public class LoginRequest
    {
        [Required]
        public string UserName { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;
    }

    public class RegisterRequest
    {
        [Required]
        public string UserName { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [MinLength(6)]
        public string Password { get; set; } = null!;
    }

    public class RefreshTokenRequest
    {
        [Required]
        public string RefreshToken { get; set; } = null!;
    }

    public class LogoutRequest
    {
        [Required]
        public string RefreshToken { get; set; } = null!;
    }

    public class EmailVerificationRequest
    {
        [Required]
        public string Token { get; set; } = null!;
    }

    public class ForgotPasswordRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
    }

    public class ResetPasswordRequest
    {
        [Required]
        public string OldPassword { get; set; } = null!;

        [Required]
        [MinLength(6)]
        public string NewPassword { get; set; } = null!;
    }

}
