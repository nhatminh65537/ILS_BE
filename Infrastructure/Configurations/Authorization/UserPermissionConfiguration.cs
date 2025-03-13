using ILS_BE.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ILS_BE.Infrastructure.Configurations
{
    public class UserPermissionConfiguration : IEntityTypeConfiguration<UserPermission>
    {
        public void Configure(EntityTypeBuilder<UserPermission> builder)
        {
            builder.HasKey(up => new { up.UserId, up.PermissionId });
            builder.HasIndex(up => up.UserId);
            builder.HasIndex(up => up.PermissionId);
            builder.HasOne<User>()
                   .WithMany()
                   .HasForeignKey(up => up.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne<Permission>()
                   .WithMany()
                   .HasForeignKey(up => up.PermissionId)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.Property(up => up.CreatedAt)
                   .HasDefaultValueSql("CURRENT_TIMESTAMP");
        }
    }
}
