using ILS_BE.Domain.DTOs;

namespace ILS_BE.Application.Interfaces
{
    public interface IRoleService : IDataService<RoleDTO>
    {
        Task<List<PermissionDTO>> GetPermissionsInRoleAsync(int roleId);
        Task AddPermissionToRoleAsync(int roleId, int permissionId);
        Task RemovePermissionFromRoleAsync(int roleId, int permissionId);
    }
}
