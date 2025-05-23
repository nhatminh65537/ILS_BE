using ILS_BE.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ILS_BE.Infrastructure.Configurations
{
    public class LessonConfiguration : IEntityTypeConfiguration<Lesson>
    {
        public void Configure(EntityTypeBuilder<Lesson> builder)
        {
            builder.HasKey(l => l.Id);
            builder.HasIndex(l => l.ContentItemId)
                   .IsUnique();
            builder.HasIndex(l => l.Title)
                   .IsUnique();
            builder.HasIndex(l => l.TypeId);
            builder.HasOne<ContentItem>()
                   .WithOne(ci => ci.Lesson)
                   .HasForeignKey<Lesson>(l => l.ContentItemId)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne<LessonType>(l => l.LessonType)
                   .WithMany()
                   .HasForeignKey(l => l.TypeId)
                   .OnDelete(DeleteBehavior.Cascade);
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
