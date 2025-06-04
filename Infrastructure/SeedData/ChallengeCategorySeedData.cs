// Infrastructure/SeedData/Challenge/ChallengeCategorySeedData.cs
using ILS_BE.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ILS_BE.Infrastructure.SeedData
{
    public class ChallengeCategorySeedData : IEntityTypeConfiguration<ChallengeCategory>
    {
        public void Configure(EntityTypeBuilder<ChallengeCategory> builder)
        {
            builder.HasData(
                new ChallengeCategory { 
                    Id = 1, 
                    Name = "Web Security", 
                    Description = "Challenges related to web security.",
                    CreatedAt = new DateTime(),
                    UpdatedAt = new DateTime()
                },
                new ChallengeCategory { 
                    Id = 2, 
                    Name = "Cryptography", 
                    Description = "Challenges related to cryptography.",
                    CreatedAt = new DateTime(),
                    UpdatedAt = new DateTime()
                },
                new ChallengeCategory { 
                    Id = 3, 
                    Name = "Reverse Engineering",
                    Description = "Challenges related to reverse engineering.",
                    CreatedAt = new DateTime(),
                    UpdatedAt = new DateTime()
                },
                new ChallengeCategory { 
                    Id = 4, 
                    Name = "Binary Exploitation", 
                    Description = "Challenges related to binary exploitation.",
                    CreatedAt = new DateTime(),
                    UpdatedAt = new DateTime()
                }
            );
        }
    }
}
