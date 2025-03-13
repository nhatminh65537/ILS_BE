using ILS_BE.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ILS_BE.Infrastructure.SeedData
{
    public class CategorySeedData : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(
                new Category { Id = 1, Name = "Crypto", Description = "Crypto related modules" },
                new Category { Id = 2, Name = "Pwn", Description = "Pwn related modules" },
                new Category { Id = 3, Name = "Rev", Description = "Rev related modules" },
                new Category { Id = 4, Name = "Web", Description = "Web related modules" }
            );
        }
    }
}
