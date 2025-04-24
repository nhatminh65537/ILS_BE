using ILS_BE.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ILS_BE.Infrastructure.Configurations
{
    public class LearnLessonTypeConfiguration : IEntityTypeConfiguration<LearnLessonType>
    {
        public void Configure(EntityTypeBuilder<LearnLessonType> builder)
        {
            builder.HasKey(lt => lt.Id);
            builder.HasIndex(lt => lt.Name)
                   .IsUnique();
        }
    }
}
