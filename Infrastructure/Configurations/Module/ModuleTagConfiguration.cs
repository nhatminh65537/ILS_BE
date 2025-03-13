using ILS_BE.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ILS_BE.Infrastructure.Configurations
{
    public class ModuleTagConfiguration : IEntityTypeConfiguration<ModuleTag>
    {
        public void Configure(EntityTypeBuilder<ModuleTag> builder)
        {
            builder.HasKey(mt => new { mt.ModuleId, mt.TagId });
            builder.HasIndex(mt => mt.ModuleId);
            builder.HasIndex(mt => mt.TagId);
            builder.HasOne<Module>()
                   .WithMany()
                   .HasForeignKey(mt => mt.ModuleId)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne<Tag>()
                   .WithMany()
                   .HasForeignKey(mt => mt.TagId)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.Property(mt => mt.CreatedAt)
                   .HasDefaultValueSql("CURRENT_TIMESTAMP");
        }
    }
}
