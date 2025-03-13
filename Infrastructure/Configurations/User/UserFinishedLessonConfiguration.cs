using ILS_BE.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ILS_BE.Infrastructure.Configurations
{
    public class UserFinishedLessonConfiguration : IEntityTypeConfiguration<UserFinishedLesson>
    {
        public void Configure(EntityTypeBuilder<UserFinishedLesson> builder)
        {
            builder.HasKey(ufl => new { ufl.UserId, ufl.LessonId });

            builder.HasIndex(ufl => ufl.UserId);
            builder.HasIndex(ufl => ufl.LessonId);

            builder.HasOne<User>()
                   .WithMany()
                   .HasForeignKey(ufl => ufl.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne<Lesson>()
                   .WithMany()
                   .HasForeignKey(ufl => ufl.LessonId)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.Property(efl => efl.CreatedAt)
                   .HasDefaultValueSql("CURRENT_TIMESTAMP");
        }
    }
}

