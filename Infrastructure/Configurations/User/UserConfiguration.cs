using ILS_BE.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ILS_BE.Infrastructure.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);
            builder.HasIndex(u => u.UserName)
                   .IsUnique();
            builder.HasIndex(u => u.Email)
                   .IsUnique();
            builder.HasIndex(u => u.PhoneNumber)
                   .IsUnique();
            builder.Property(u => u.EmailVerified)
                   .HasDefaultValue(false);
            builder.Property(u => u.RequirePasswordReset)
                   .HasDefaultValue(false);
            builder.Property(u => u.CreatedAt)
                   .HasDefaultValueSql("CURRENT_TIMESTAMP");
            builder.Property(u => u.UpdatedAt)
                   .HasDefaultValueSql("CURRENT_TIMESTAMP")
                   .ValueGeneratedOnAddOrUpdate();
            builder.HasMany(u => u.Roles)
                   .WithMany()
                   .UsingEntity<UserRole>();
            builder.HasMany(u => u.Permissions)
                   .WithMany()
                   .UsingEntity<UserPermission>();
        }
    }
}
