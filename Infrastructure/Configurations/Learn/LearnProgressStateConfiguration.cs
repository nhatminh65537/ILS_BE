using ILS_BE.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ILS_BE.Infrastructure.Configurations
{
    public class LearnProgressStateConfiguration : IEntityTypeConfiguration<LearnProgressState>
    {
        public void Configure(EntityTypeBuilder<LearnProgressState> builder)
        {
            builder.HasKey(ps => ps.Id);
            builder.HasIndex(ps => ps.Name)
                   .IsUnique();
        }
    }
}
