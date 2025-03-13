using ILS_BE.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ILS_BE.Infrastructure.Configurations
{
    public class ExternalLoginConfiguration : IEntityTypeConfiguration<ExternalLogin>
    {
        public void Configure(EntityTypeBuilder<ExternalLogin> builder)
        {
            builder.HasKey(el => new { el.UserId, el.Provider });
            builder.HasIndex(el => el.ProviderUserId);
            builder.HasOne<User>()
                   .WithMany()
                   .HasForeignKey(el => el.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.Property(el => el.CreatedAt)
                   .HasDefaultValueSql("CURRENT_TIMESTAMP");
            builder.Property(el => el.UpdatedAt)
                   .HasDefaultValueSql("CURRENT_TIMESTAMP")
                   .ValueGeneratedOnAddOrUpdate();
        }
    }
}
