using ILS_BE.Domain.Interfaces;
using ILS_BE.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;


namespace ILS_BE.Application.Authorization
{
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly IGenericRepository<UserEffectivePermission> _userEffectivePermissionRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PermissionHandler(IGenericRepository<UserEffectivePermission> userEffectivePermissionRepository, IHttpContextAccessor httpContextAccessor)
        {
            _userEffectivePermissionRepository = userEffectivePermissionRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim))
            {
                context.Fail();
                return;
            }

            if (!int.TryParse(userIdClaim, out int userId))
            {
                throw new InvalidOperationException("User ID is invalid.");
            }

            
            var hasPermission = await _userEffectivePermissionRepository.GetFirstWhereAsync(upv => upv.UserId == userId && upv.PermissionId == requirement.PermissionId);
            if (hasPermission != null)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
        }
    }
}
