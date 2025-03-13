using ILS_BE.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ILS_BE.Infrastructure.SeedData
{

    public class UserSeedData : IEntityTypeConfiguration<User>
    {
        private readonly IConfiguration _configuration;
        public UserSeedData(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void Configure(EntityTypeBuilder<User> builder)
        {
            var newUser = new User
            {
                Id = 1,
                UserName = _configuration["AdminAccount:UserName"]!,
                Email = _configuration["AdminAccount:Email"]!,
                PasswordHash = _configuration["AdminAccount:PasswordHash"]!,
                CreatedAt = new DateTime(),
                UpdatedAt = new DateTime()
            };
            builder.HasData(newUser);
        }
    }
}
