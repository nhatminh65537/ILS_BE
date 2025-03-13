using ILS_BE.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ILS_BE.Infrastructure.SeedData
{
    public class LifecycleStateSeedData : IEntityTypeConfiguration<LifecycleState>
    {
        public void Configure(EntityTypeBuilder<LifecycleState> builder)
        {
            builder.HasData(
                new LifecycleState { Id = 1, Name = "Creating", Description = "Module is being created" },
                new LifecycleState { Id = 2, Name = "Updating", Description = "Module is being updated" },
                new LifecycleState { Id = 3, Name = "Published", Description = "Module is published" }
            );
        }
    }
}
