// Infrastructure/Configurations/Challenge/ChallengeFileConfiguration.cs
using ILS_BE.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ILS_BE.Infrastructure.Configurations
{
    public class ChallengeFileConfiguration : IEntityTypeConfiguration<ChallengeFile>
    {
        public void Configure(EntityTypeBuilder<ChallengeFile> builder)
        {
            builder.ToTable("challenge_files");
            builder.HasKey(x => x.Id);
            
            builder.Property(x => x.FileName).IsRequired();
            builder.Property(x => x.FilePath).IsRequired();
            
            builder.HasOne<ChallengeProblem>()
                .WithMany(x => x.Files)
                .HasForeignKey(x => x.ChallengeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
