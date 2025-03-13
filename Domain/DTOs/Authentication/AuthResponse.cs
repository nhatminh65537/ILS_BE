namespace ILS_BE.Domain.DTOs
{
    public class AuthResponse
    {
        public string Token { get; set; } = null!;
        public int ExpiresIn { get; set; }
    }
}
