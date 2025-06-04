// Infrastructure/Configurations/Challenge/ChallengeStateConfiguration.cs
using ILS_BE.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ILS_BE.Infrastructure.Configurations
{
    public class ChallengeStateConfiguration : IEntityTypeConfiguration<ChallengeState>
    {
        public void Configure(EntityTypeBuilder<ChallengeState> builder)
        {
            builder.ToTable("challenge_states");
            builder.HasKey(x => x.Id);
            
            builder.Property(x => x.Name).IsRequired();
            builder.HasIndex(x => x.Name).IsUnique();
        }
    }
}
