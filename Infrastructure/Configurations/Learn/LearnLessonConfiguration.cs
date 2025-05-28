using ILS_BE.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ILS_BE.Infrastructure.Configurations
{
    public class LearnLessonConfiguration : IEntityTypeConfiguration<LearnLesson>
    {
        public void Configure(EntityTypeBuilder<LearnLesson> builder)
        {
            builder.HasKey(l => l.Id);
            builder.HasIndex(l => l.Title)
                   .IsUnique();
            builder.HasIndex(l => l.TypeId);
            builder.HasOne(l => l.LessonType)
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
