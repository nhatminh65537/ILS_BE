using ILS_BE.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ILS_BE.Infrastructure.Configurations
{
    public class LearnTagConfiguration : IEntityTypeConfiguration<LearnTag>
    {
        public void Configure(EntityTypeBuilder<LearnTag> builder)
        {
            builder.HasKey(t => t.Id);
            builder.HasIndex(t => t.Name)
                   .IsUnique();
        }
    }
}
