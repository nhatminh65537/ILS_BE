using ILS_BE.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ILS_BE.Infrastructure.SeedData
{
    public class CategorySeedData : IEntityTypeConfiguration<LearnCategory>
    {
        public void Configure(EntityTypeBuilder<LearnCategory> builder)
        {
            builder.HasData(
                new LearnCategory { Id = 1, Name = "Crypto", Description = "Crypto related modules" },
                new LearnCategory { Id = 2, Name = "Pwn", Description = "Pwn related modules" },
                new LearnCategory { Id = 3, Name = "Rev", Description = "Rev related modules" },
                new LearnCategory { Id = 4, Name = "Web", Description = "Web related modules" }
            );
        }
    }
}
