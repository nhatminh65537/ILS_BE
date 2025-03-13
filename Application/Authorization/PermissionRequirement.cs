using Microsoft.AspNetCore.Authorization;

namespace ILS_BE.Application.Authorization
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public int PermissionId { get; }

        public PermissionRequirement(int permissionId)
        {
            PermissionId = permissionId;
        }
    }
}
