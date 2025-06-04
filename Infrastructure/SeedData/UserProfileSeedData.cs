using ILS_BE.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ILS_BE.Infrastructure.SeedData
{
    public class UserProfileSeedData : IEntityTypeConfiguration<UserProfile>
    {
        public void Configure(EntityTypeBuilder<UserProfile> builder)
        {
            var userProfile = new UserProfile
            {
                Id = 1,
                FirstName = "Admin",
                LastName = "Admin",
                DisplayName = "Admin",
                Xp = 0,
                Level = 1,
                AvatarPath = null,
                Bio = "This is the admin user profile.",
            };
            builder.HasData(userProfile);
        }
    }
}
