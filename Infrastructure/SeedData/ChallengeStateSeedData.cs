// Infrastructure/SeedData/Challenge/ChallengeStateSeedData.cs
using ILS_BE.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ILS_BE.Infrastructure.SeedData
{
    public class ChallengeStateSeedData : IEntityTypeConfiguration<ChallengeState>
    {
        public void Configure(EntityTypeBuilder<ChallengeState> builder)
        {
            builder.HasData(
                new ChallengeState { 
                    Id = 1, 
                    Name = "Open", 
                    Description = "Challenge is open for participation.",
                    CreatedAt = new DateTime(),
                    UpdatedAt = new DateTime()
                },
                new ChallengeState { 
                    Id = 2, 
                    Name = "Closed", 
                    Description = "Challenge is closed.",
                    CreatedAt = new DateTime(),
                    UpdatedAt = new DateTime()
                }
            );
        }
    }
}
