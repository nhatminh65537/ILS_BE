using ILS_BE.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ILS_BE.Infrastructure.Configurations
{
    public class LearnModuleTagConfiguration : IEntityTypeConfiguration<LearnModuleTag>
    {
        public void Configure(EntityTypeBuilder<LearnModuleTag> builder)
        {
            builder.HasKey(mt => new { mt.ModuleId, mt.TagId });
            builder.HasIndex(mt => mt.ModuleId);
            builder.HasIndex(mt => mt.TagId);
            
            

            builder.Property(mt => mt.CreatedAt)
                   .HasDefaultValueSql("CURRENT_TIMESTAMP");
        }
    }
}
