using ILS_BE.Domain.DTOs;
using ILS_BE.Application.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ILS_BE.Domain.Interfaces;
using ILS_BE.Domain.Models;

namespace ILS_BE.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<UserProfile> _userProfileRepository;
        private readonly IPasswordService _passwordService;
        private readonly ITokenService _tokenService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(
            IRepository<User> userRepository,
            IRepository<UserProfile> userProfileRepository,
            IPasswordService passwordService, 
            ITokenService tokenService,
            IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _userProfileRepository = userProfileRepository;
            _passwordService = passwordService;
            _tokenService = tokenService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<AuthResponse?> LoginAsync(LoginRequest request)
        {
            var user = await _userRepository.GetFirstWhereAsync(u => u.UserName == request.UserName);
            if (user == null)
            {
                return null;
            }
            if (_passwordService.VerifyUserPassword(user, request.Password))
            {
                var token = _tokenService.GenerateJwtToken(user.Id);
                return new AuthResponse { Token = token };
            }
            else
            {
                return null;
            }
        }

        public async Task LogoutAsync()
        {
            var jti = _httpContextAccessor.HttpContext?.User?.FindFirstValue(JwtRegisteredClaimNames.Jti);

            await _tokenService.RevokeTokenAsync(jti!);
        }

        public Task<AuthResponse> RefreshTokenAsync(RefreshTokenRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RegisterAsync(RegisterRequest request)
        {
            var testUserName = await _userRepository.GetFirstWhereAsync(u => u.UserName == request.UserName);
            var testEmail = await _userRepository.GetFirstWhereAsync(u => u.Email == request.Email);

            if (testUserName != null || testEmail != null)
            {
                return false;
            }

            var newUser = _passwordService.CreateUserWithHashedPassword(
                new User { UserName = request.UserName, Email = request.Email },
                request.Password);
            var user = await _userRepository.AddAsync(newUser);
            await _userRepository.SaveAsync();

            var userProfile = new UserProfile {
                Id = user.Id,
                DisplayName = request.UserName 
            };
            await _userProfileRepository.AddAsync(userProfile);
            await _userProfileRepository.SaveAsync();

            return true;
        }

        public async Task<bool> ResetPasswordAsync(ResetPasswordRequest request)
        {
            var userId = GetUserId();
            User user = (await _userRepository.GetByIdAsync(userId))!;
            if (_passwordService.VerifyUserPassword(user, request.OldPassword))
            {
                await _userRepository.UpdateAsync(
                    _passwordService.CreateUserWithHashedPassword(user, request.NewPassword)   
                );
                await _userRepository.SaveAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public Task SendPasswordResetEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<bool> VerifyEmailAsync(string token)
        {
            throw new NotImplementedException();
        }
        public int GetUserId()
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userIdClaim, out int userId))
            {
                throw new Exception("Invalid userId");
            }
            return userId;
        }
    }
}
