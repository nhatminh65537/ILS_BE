using ILS_BE.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ILS_BE.Infrastructure.Configurations
{
    public class UserProfileConfiguration : IEntityTypeConfiguration<UserProfile>
    {
        public void Configure(EntityTypeBuilder<UserProfile> builder)
        {
            builder.HasKey(up => up.Id);
            builder.HasOne<User>()
                   .WithOne(u => u.Profile)
                   .HasForeignKey<UserProfile>(up => up.Id)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.HasIndex(up => up.DisplayName)
                   .IsUnique();
            builder.Property(up => up.Xp)
                   .HasDefaultValue(0);
            builder.Property(up => up.Level)
                   .HasDefaultValue(1);
            builder.Property(up => up.UpdatedAt)
                   .HasDefaultValueSql("CURRENT_TIMESTAMP")
                   .ValueGeneratedOnAddOrUpdate();
        }
    }
}
