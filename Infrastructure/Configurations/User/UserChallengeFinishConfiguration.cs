// Infrastructure/Configurations/Challenge/UserChallengeFinishConfiguration.cs
using ILS_BE.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ILS_BE.Infrastructure.Configurations
{
    public class UserChallengeFinishConfiguration : IEntityTypeConfiguration<UserChallengeFinish>
    {
        public void Configure(EntityTypeBuilder<UserChallengeFinish> builder)
        {
            builder.ToTable("user_challenge_finishes");
            builder.HasKey(x => new { x.UserId, x.ChallengeId });
            
            builder.Property(x => x.FinishedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            
            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
                
            builder.HasOne<ChallengeProblem>()
                .WithMany()
                .HasForeignKey(x => x.ChallengeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
