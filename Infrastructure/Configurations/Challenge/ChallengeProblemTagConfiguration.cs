// Infrastructure/Configurations/Challenge/ChallengeProblemTagConfiguration.cs
using ILS_BE.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ILS_BE.Infrastructure.Configurations
{
    public class ChallengeProblemTagConfiguration : IEntityTypeConfiguration<ChallengeProblemTag>
    {
        public void Configure(EntityTypeBuilder<ChallengeProblemTag> builder)
        {
            builder.ToTable("challenge_problem_tags");
            builder.HasKey(x => new { x.ChallengeProblemId, x.ChallengeTagId });
            
            builder.Property(x => x.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            
            builder.HasOne<ChallengeProblem>()
                .WithMany()
                .HasForeignKey(x => x.ChallengeProblemId)
                .OnDelete(DeleteBehavior.Cascade);
                
            builder.HasOne<ChallengeProblem>()
                .WithMany()
                .HasForeignKey(x => x.ChallengeTagId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
