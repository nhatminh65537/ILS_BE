// Infrastructure/SeedData/Challenge/ChallengeNodeSeedData.cs
using ILS_BE.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace ILS_BE.Infrastructure.SeedData
{
    public class ChallengeNodeSeedData : IEntityTypeConfiguration<ChallengeNode>
    {
        public void Configure(EntityTypeBuilder<ChallengeNode> builder)
        {
            builder.HasData(
                new ChallengeNode
                {
                    Id = 1,
                    Path = ".",
                    Title = "Root Challenge Node",
                    IsProblem = false,
                    CreatedAt = new DateTime(),
                    UpdatedAt = new DateTime()
                }
            );
        }
    }
}
