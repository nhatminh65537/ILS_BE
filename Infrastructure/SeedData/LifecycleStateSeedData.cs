using ILS_BE.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ILS_BE.Infrastructure.SeedData
{
    public class LifecycleStateSeedData : IEntityTypeConfiguration<LearnLifecycleState>
    {
        public void Configure(EntityTypeBuilder<LearnLifecycleState> builder)
        {
            builder.HasData(
                new LearnLifecycleState { Id = 1, Name = "Creating", Description = "Module is being created" },
                new LearnLifecycleState { Id = 2, Name = "Updating", Description = "Module is being updated" },
                new LearnLifecycleState { Id = 3, Name = "Published", Description = "Module is published" }
            );
        }
    }
}
