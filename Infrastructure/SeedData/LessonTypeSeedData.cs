using ILS_BE.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ILS_BE.Infrastructure.SeedData
{
    public class LessonTypeSeedData : IEntityTypeConfiguration<LessonType>
    {
        public void Configure(EntityTypeBuilder<LessonType> builder)
        {
            builder.HasData(
                new LessonType { Id = 1, Name = "Markdown", Description = "Markdown lesson" },
                new LessonType { Id = 2, Name = "Video", Description = "Video lesson" },
                new LessonType { Id = 3, Name = "Quiz", Description = "Quiz lesson" }
            );
        }
    }
}
