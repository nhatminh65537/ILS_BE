using ILS_BE.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ILS_BE.Infrastructure.SeedData
{
    public class ProgressStateSeedData : IEntityTypeConfiguration<ProgressState>
    {
        public void Configure(EntityTypeBuilder<ProgressState> builder)
        {
            builder.HasData(
                new ProgressState { Id = 1, Name = "Locked", Description = "Module is not started" },
                new ProgressState { Id = 2, Name = "Learning", Description = "Module is being learned" },
                new ProgressState { Id = 3, Name = "Completed", Description = "Module is completed" }
            );
        }
    }
}
