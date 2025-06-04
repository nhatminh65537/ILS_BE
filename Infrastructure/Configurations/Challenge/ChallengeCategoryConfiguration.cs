// Infrastructure/Configurations/Challenge/ChallengeCategoryConfiguration.cs
using ILS_BE.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ILS_BE.Infrastructure.Configurations
{
    public class ChallengeCategoryConfiguration : IEntityTypeConfiguration<ChallengeCategory>
    {
        public void Configure(EntityTypeBuilder<ChallengeCategory> builder)
        {
            builder.ToTable("challenge_categories");
            builder.HasKey(x => x.Id);
            
            builder.Property(x => x.Name).IsRequired();
            builder.HasIndex(x => x.Name).IsUnique();
        }
    }
}
