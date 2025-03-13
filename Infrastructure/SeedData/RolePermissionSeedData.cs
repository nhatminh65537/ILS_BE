using ILS_BE.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ILS_BE.Infrastructure.SeedData
{
    public class RolePermissionSeedData : IEntityTypeConfiguration<RolePermission>
    {
        private readonly List<Permission> permissions;
        public RolePermissionSeedData(List<Permission> permissions)
        {
            this.permissions = permissions;
        }
        public void Configure(EntityTypeBuilder<RolePermission> builder)
        {
            foreach (var permission in permissions)
            {
                builder.HasData(
                    new RolePermission
                    {
                        RoleId = 1,
                        PermissionId = permission.Id,
                        CreatedAt = new DateTime()
                    }
                );
            }
        }
    }
}
