// Infrastructure/Configurations/Challenge/ChallengeTagConfiguration.cs
using ILS_BE.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ILS_BE.Infrastructure.Configurations
{
    public class ChallengeTagConfiguration : IEntityTypeConfiguration<ChallengeTag>
    {
        public void Configure(EntityTypeBuilder<ChallengeTag> builder)
        {
            builder.ToTable("challenge_tags");
            builder.HasKey(x => x.Id);
            
            builder.Property(x => x.Name).IsRequired();
            builder.HasIndex(x => x.Name).IsUnique();
        }
    }
}
