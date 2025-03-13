using ILS_BE.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ILS_BE.Infrastructure.SeedData
{
    public class UserRoleSeedData : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<UserRole> builder)
        {
            builder.HasData(
                new UserRole { UserId = 1, RoleId = 1, CreatedAt = new DateTime() }
            );
        }
    }
}
