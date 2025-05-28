using ILS_BE.Domain.Interfaces;
using ILS_BE.Domain.Models;
using ILS_BE.Infrastructure;
using ILS_BE.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ILS_BE.Application.Authorization
{
    public class UserPermissionStore
    {
        private readonly HashSet<(int userId, int permissionId)> _permissions = new();

        private readonly IServiceScopeFactory _scopeFactory;

        public UserPermissionStore(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public bool HasPermission(int userId, int permissionId)
            => _permissions.Contains((userId, permissionId));

        public async Task LoadPermissions()
        {
            using var scope = _scopeFactory.CreateScope();
            var repo = scope.ServiceProvider.GetRequiredService<UserEffectivePermissionRepository>();

            var userPermissions = await repo.GetAllUserEffectivePermissionsAsync();

            foreach (var userPermission in userPermissions)
            {
                _permissions.Add((userPermission.UserId, userPermission.PermissionId));
            }
        }
    }
}
