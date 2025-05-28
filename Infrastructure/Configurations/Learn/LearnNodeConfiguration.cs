using ILS_BE.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ILS_BE.Infrastructure.Configurations
{
    public class LearnNodeConfiguration : IEntityTypeConfiguration<LearnNode>
    {
        public void Configure(EntityTypeBuilder<LearnNode> builder)
        {
            builder.HasKey(ci => ci.Id);

            builder.HasIndex(ci => ci.Title)
                   .IsUnique();
            builder.HasIndex(ci => ci.LessonId)
                   .IsUnique();
            builder.HasOne(ci => ci.Lesson)
                   .WithOne()
                   .HasForeignKey<LearnNode>(l => l.LessonId)
                   .OnDelete(DeleteBehavior.Cascade);
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
