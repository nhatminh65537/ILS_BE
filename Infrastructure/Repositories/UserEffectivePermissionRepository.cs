using ILS_BE.Domain.Interfaces;
using ILS_BE.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ILS_BE.Infrastructure.Repositories
{
    public class UserEffectivePermissionRepository
    {
        private readonly AppDbContext _context;
        public UserEffectivePermissionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserEffectivePermission>> GetAllUserEffectivePermissionsAsync()
        {
            var userPermissions = await _context.UserPermissions
                .Select(up => new UserEffectivePermission { UserId = up.UserId, PermissionId = up.PermissionId })
                .Union(
                    from ur in _context.UserRoles
                    join rp in _context.RolePermissions on ur.RoleId equals rp.RoleId
                    select new UserEffectivePermission { UserId = ur.UserId, PermissionId = rp.PermissionId })
                .ToListAsync();
            return userPermissions;
        }

        public async Task<List<UserEffectivePermission>> GetUserEffectivePermissionsAsync(int userId)
        {
            var userPermissions = await _context.UserPermissions
                .Select(up => new UserEffectivePermission  { UserId = up.UserId, PermissionId = up.PermissionId })
                .Union(
                    from ur in _context.UserRoles
                    join rp in _context.RolePermissions on ur.RoleId equals rp.RoleId
                    select new UserEffectivePermission { UserId = ur.UserId, PermissionId = rp.PermissionId })
                .ToListAsync();

            return userPermissions
                .Where(up => up.UserId == userId)
                .ToList();
        }
    }
}
