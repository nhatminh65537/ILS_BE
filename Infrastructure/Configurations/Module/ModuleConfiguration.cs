using ILS_BE.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ILS_BE.Infrastructure.Configurations
{
    public class ModuleConfiguration : IEntityTypeConfiguration<Module>
    {
        public void Configure(EntityTypeBuilder<Module> builder)
        {
            builder.HasKey(m => m.Id);

            builder.HasIndex(m => m.ContentItemId)
                   .IsUnique();
            builder.HasIndex(m => m.Title)
                   .IsUnique();
            builder.HasIndex(m => m.CreatedBy);
            builder.HasIndex(m => m.CategoryId);
            builder.HasIndex(m => m.LifecycleStateId);

            builder.HasOne<ContentItem>()
                   .WithOne(ci => ci.Module)
                   .HasForeignKey<Module>(m => m.ContentItemId)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne<User>()
                   .WithMany()
                   .HasForeignKey(m => m.CreatedBy)
                   .OnDelete(DeleteBehavior.SetNull);
            builder.HasOne<Category>(m => m.Category)
                   .WithMany()
                   .HasForeignKey(m => m.CategoryId)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne<LifecycleState>(m => m.LifecycleState)
                   .WithMany()
                   .HasForeignKey(m => m.LifecycleStateId)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany<Tag>(m => m.Tags)
                   .WithMany()
                   .UsingEntity<ModuleTag>();

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
