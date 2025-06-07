using ILS_BE.Domain.DTOs;

namespace ILS_BE.Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponse?> LoginAsync(LoginRequest request);
        Task<bool> RegisterAsync(RegisterRequest request);
        Task<AuthResponse> RefreshTokenAsync(RefreshTokenRequest request);
        Task LogoutAsync();
        Task<bool> VerifyEmailAsync(string token);
        Task SendPasswordResetEmailAsync(string email);
        Task<bool> ChangePasswordAsync(ChangePasswordRequest request);
        int GetUserId();
    }
}