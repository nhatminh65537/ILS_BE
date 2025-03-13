using ILS_BE.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ILS_BE.Infrastructure.Configurations
{
    public class UserModuleProgressConfiguration : IEntityTypeConfiguration<UserModuleProgress>
    {
        public void Configure(EntityTypeBuilder<UserModuleProgress> builder)
        {
            builder.HasKey(ump => new { ump.UserId, ump.ModuleId });

            builder.HasIndex(ump => ump.UserId);
            builder.HasIndex(ump => ump.ModuleId);
            builder.HasIndex(ump => ump.ProgressStateId);

            builder.HasOne<User>()
                   .WithMany()
                   .HasForeignKey(ump => ump.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne<Module>()
                   .WithMany()
                   .HasForeignKey(ump => ump.ModuleId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne<ProgressState>()
                   .WithMany()
                   .HasForeignKey(ump => ump.ProgressStateId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

