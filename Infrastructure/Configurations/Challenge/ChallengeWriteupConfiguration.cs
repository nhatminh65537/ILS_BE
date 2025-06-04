// Infrastructure/Configurations/Challenge/ChallengeWriteupConfiguration.cs
using ILS_BE.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ILS_BE.Infrastructure.Configurations
{
    public class ChallengeWriteupConfiguration : IEntityTypeConfiguration<ChallengeWriteup>
    {
        public void Configure(EntityTypeBuilder<ChallengeWriteup> builder)
        {
            builder.ToTable("challenge_writeups");
            builder.HasKey(x => x.Id);
            
            builder.Property(x => x.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            builder.Property(x => x.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            
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
