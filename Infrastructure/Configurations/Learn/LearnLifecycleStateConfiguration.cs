using ILS_BE.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ILS_BE.Infrastructure.Configurations
{
    public class LearnLifecycleStateConfiguration : IEntityTypeConfiguration<LearnLifecycleState>
    {
        public void Configure(EntityTypeBuilder<LearnLifecycleState> builder)
        {
            builder.HasKey(ls => ls.Id);
            builder.HasIndex(ls => ls.Name)
                   .IsUnique();
        }
    }
}
