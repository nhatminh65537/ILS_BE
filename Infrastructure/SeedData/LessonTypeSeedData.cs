using ILS_BE.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ILS_BE.Infrastructure.SeedData
{
    public class LessonTypeSeedData : IEntityTypeConfiguration<LearnLessonType>
    {
        public void Configure(EntityTypeBuilder<LearnLessonType> builder)
        {
            builder.HasData(
                new LearnLessonType { Id = 1, Name = "Markdown", Description = "Markdown lesson" },
                new LearnLessonType { Id = 2, Name = "Video", Description = "Video lesson" },
                new LearnLessonType { Id = 3, Name = "Quiz", Description = "Quiz lesson" }
            );
        }
    }
}
