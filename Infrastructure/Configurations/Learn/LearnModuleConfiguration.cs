using ILS_BE.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ILS_BE.Infrastructure.Configurations
{
    public class LearnModuleConfiguration : IEntityTypeConfiguration<LearnModule>
    {
        public void Configure(EntityTypeBuilder<LearnModule> builder)
        {
            builder.HasKey(m => m.Id);

            builder.HasIndex(m => m.NodeId)
                   .IsUnique();
            builder.HasIndex(m => m.Title)
                   .IsUnique();
            builder.HasIndex(m => m.CreatedBy);
            builder.HasIndex(m => m.CategoryId);
            builder.HasIndex(m => m.LifecycleStateId);

            builder.HasOne(m => m.Node)
                   .WithOne()
                   .HasForeignKey<LearnModule>(m => m.NodeId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne<User>()
                   .WithMany()
                   .HasForeignKey(m => m.CreatedBy)
                   .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(m => m.Category)
                   .WithMany()
                   .HasForeignKey(m => m.CategoryId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(m => m.LifecycleState)
                   .WithMany()
                   .HasForeignKey(m => m.LifecycleStateId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(m => m.Tags)
                   .WithMany()
                   .UsingEntity<LearnModuleTag>(
                b => b.HasOne<LearnTag>()
                   .WithMany()
                   .HasForeignKey(mt => mt.TagId)
                   .OnDelete(DeleteBehavior.Cascade),
                b => b.HasOne<LearnModule>()
                   .WithMany()
                   .HasForeignKey(mt => mt.ModuleId)
                   .OnDelete(DeleteBehavior.Cascade));

            builder.Property(l => l.Xp)
                   .HasDefaultValue(0);
            builder.Property(l => l.Duration)
                   .HasDefaultValue(0);
            builder.Property(l => l.CreatedAt)
                   .HasDefaultValueSql("CURRENT_TIMESTAMP");
            builder.Property(l => l.UpdatedAt)
                   .HasDefaultValueSql("CURRENT_TIMESTAMP")
                   .ValueGeneratedOnAddOrUpdate();
        }
    }
}
