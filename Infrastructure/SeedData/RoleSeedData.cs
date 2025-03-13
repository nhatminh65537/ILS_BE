using ILS_BE.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ILS_BE.Infrastructure.SeedData
{
    public class RoleSeedData : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasData(
                new Role { Id = 1, Name = "Admin", Changeable = false },
                new Role { Id = 2, Name = "Collaborator" , Changeable = false },
                new Role { Id = 3, Name = "User" , Changeable = false }
            );
        }
    }
}
