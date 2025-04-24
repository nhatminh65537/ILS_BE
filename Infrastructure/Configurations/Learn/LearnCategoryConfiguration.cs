using ILS_BE.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ILS_BE.Infrastructure.Configurations
{
    public class LearnCategoryConfiguration : IEntityTypeConfiguration<LearnCategory>
    {
        public void Configure(EntityTypeBuilder<LearnCategory> builder)
        {
            builder.HasKey(c => c.Id);
            builder.HasIndex(c => c.Name)
                   .IsUnique();
        }
    }
}
