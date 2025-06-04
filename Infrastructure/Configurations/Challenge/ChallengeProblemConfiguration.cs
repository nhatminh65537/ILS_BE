// Infrastructure/Configurations/Challenge/ChallengeProblemConfiguration.cs
using ILS_BE.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ILS_BE.Infrastructure.Configurations
{
    public class ChallengeProblemConfiguration : IEntityTypeConfiguration<ChallengeProblem>
    {
        public void Configure(EntityTypeBuilder<ChallengeProblem> builder)
        {
            builder.ToTable("challenge_problems");
            builder.HasKey(x => x.Id);
            
            builder.Property(x => x.Title).IsRequired();
            builder.Property(x => x.Flag).IsRequired();
            builder.Property(x => x.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            builder.Property(x => x.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            
            builder.HasOne(x => x.ChallengeState)
                .WithMany()
                .HasForeignKey(x => x.ChallengeStateId)
                .OnDelete(DeleteBehavior.Restrict);
                
            builder.HasOne(x => x.Category)
                .WithMany()
                .HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
                
            builder.HasMany(x => x.Files)
                .WithOne()
                .HasForeignKey(x => x.ChallengeId)
                .OnDelete(DeleteBehavior.Cascade);
                
            builder.HasMany(x => x.Tags)
                .WithMany()
                .UsingEntity<ChallengeProblemTag>(
                    j => j.HasOne< ChallengeTag>().WithMany().HasForeignKey(pt => pt.ChallengeTagId).OnDelete(DeleteBehavior.Cascade),
                    j => j.HasOne<ChallengeProblem>().WithMany().HasForeignKey(pt => pt.ChallengeProblemId).OnDelete(DeleteBehavior.Cascade),
                    j => {
                        j.ToTable("challenge_problem_tags");
                        j.HasKey(t => new { t.ChallengeProblemId, t.ChallengeTagId });
                        j.Property(t => t.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
                    }
                );
        }
    }
}
