using ILS_BE.Domain.Models;

namespace ILS_BE.Domain.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetUserDetailAsync(int id);
        User? GetUserDetail(int id);
        Task<List<Permission>> GetUserPermissionsAsync(int userId);
        Task<List<Role>> GetUserRolesAsync(int userId);
        Task<UserProfile> GetUserProfileAsync(int userId);
        List<Permission> GetUserPermissions(int userId);
        List<Role> GetUserRoles(int userId);
        UserProfile GetUserProfile(int userId);
    }
}
