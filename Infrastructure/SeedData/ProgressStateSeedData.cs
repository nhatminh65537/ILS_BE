using ILS_BE.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ILS_BE.Infrastructure.SeedData
{
    public class ProgressStateSeedData : IEntityTypeConfiguration<LearnProgressState>
    {
        public void Configure(EntityTypeBuilder<LearnProgressState> builder)
        {
            builder.HasData(
                new LearnProgressState { Id = 1, Name = "Locked", Description = "Module is not started" },
                new LearnProgressState { Id = 2, Name = "Learning", Description = "Module is being learned" },
                new LearnProgressState { Id = 3, Name = "Completed", Description = "Module is completed" }
            );
        }
    }
}
