using ILS_BE.Domain.DTOs;

namespace ILS_BE.Application.Interfaces
{
    public interface IUserService : IDataService<UserDTO>
    {
        Task<UserDTO> GetByUsernameAsync(string username);
        Task<UserDTO> GetByEmailAsync(string email);
        Task<UserProfileDTO> GetUserProfileAsync(int userId);
        Task<List<RoleDTO>> GetRolesOfUserAsync(int userId);
        Task AddRoleToUserAsync(int userId, int roleId);
        Task RemoveRoleFromUserAsync(int userId, int roleId);
        Task<List<PermissionDTO>> GetEffectivePermissionsOfUserAsync(int userId);
        Task AddPermissionToUserAsync(int userId, int permissionId);
        Task RemovePermissionFromUserAsync(int userId, int permissionId);
    }
}
