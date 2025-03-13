using ILS_BE.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ILS_BE.Infrastructure.Configurations
{
    public class UserEffectivePermissionConfiguration : IEntityTypeConfiguration<UserEffectivePermission>
    {
        public void Configure(EntityTypeBuilder<UserEffectivePermission> builder)
        {
            builder.HasNoKey();
            builder.ToView("user_effective_permissions");
        }
    }
}
