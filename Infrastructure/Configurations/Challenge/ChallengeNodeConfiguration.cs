// Infrastructure/Configurations/Challenge/ChallengeNodeConfiguration.cs
using ILS_BE.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ILS_BE.Infrastructure.Configurations
{
    public class ChallengeNodeConfiguration : IEntityTypeConfiguration<ChallengeNode>
    {
        public void Configure(EntityTypeBuilder<ChallengeNode> builder)
        {
            builder.ToTable("challenge_nodes");
            builder.HasKey(x => x.Id);
            
            builder.Property(x => x.Path).IsRequired();
            builder.Property(x => x.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            builder.Property(x => x.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            builder.Property(x => x.IsProblem).HasDefaultValue(false);
            
            builder.HasOne(x => x.Problem)
                .WithOne()
                .HasForeignKey<ChallengeNode>(x => x.ProblemId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
