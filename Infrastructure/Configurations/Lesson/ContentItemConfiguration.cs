using ILS_BE.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ILS_BE.Infrastructure.Configurations
{
    public class ContentItemConfiguration : IEntityTypeConfiguration<ContentItem>
    {
        public void Configure(EntityTypeBuilder<ContentItem> builder)
        {
            builder.HasKey(ci => ci.Id);

            builder.HasIndex(ci => ci.Title)
                   .IsUnique();
            builder.HasIndex(ci => ci.ModuleId)
                   .IsUnique();
            builder.HasIndex(ci => ci.LessonId)
                   .IsUnique();
            builder.Property(ci => ci.IsModule)
                   .HasDefaultValue(false);
            builder.Property(ci => ci.IsLesson)
                   .HasDefaultValue(false);
            builder.Property(ci => ci.Order)
                   .HasDefaultValue(0);
            builder.Property(ci => ci.CreatedAt)
                   .HasDefaultValueSql("CURRENT_TIMESTAMP");
            builder.Property(ci => ci.UpdatedAt)
                   .HasDefaultValueSql("CURRENT_TIMESTAMP")
                   .ValueGeneratedOnAddOrUpdate();
        }
    }
}
