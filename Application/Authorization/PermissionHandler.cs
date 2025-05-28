using ILS_BE.Domain.Interfaces;
using ILS_BE.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;


namespace ILS_BE.Application.Authorization
{
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly UserPermissionStore _userPermissionStore;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PermissionHandler(UserPermissionStore userPermissionStore, IHttpContextAccessor httpContextAccessor)
        {
            _userPermissionStore = userPermissionStore;
            _httpContextAccessor = httpContextAccessor;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim))
            {
                context.Fail();
                return Task.CompletedTask;
            }

            if (!int.TryParse(userIdClaim, out int userId))
            {
                throw new InvalidOperationException("User ID is invalid.");
            }

            
            var hasPermission = _userPermissionStore.HasPermission(userId, requirement.PermissionId);
            if (hasPermission)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }

            return Task.CompletedTask;
        }
    }
}
