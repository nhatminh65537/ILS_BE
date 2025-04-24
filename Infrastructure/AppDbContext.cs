using ILS_BE.Domain.Models;
using Microsoft.EntityFrameworkCore;
using ILS_BE.Infrastructure.Configurations;
using ILS_BE.Infrastructure.SeedData;
using System.Runtime.CompilerServices;
using System.ComponentModel;

namespace ILS_BE.Infrastructure
{
    public class AppDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        private readonly DbContextOptions<AppDbContext> _options;

        public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration)
            : base(options)
        {
            _options = options;
            _configuration = configuration;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<ExternalLogin> ExternalLogins { get; set; }
        public DbSet<UserEffectivePermission> UserEffectivePermissions { get; set; }
        public DbSet<UserPermission> UserPermissions { get; set; }

        public DbSet<LearnModule> Modules { get; set; }
        public DbSet<LearnCategory> Categories { get; set; }
        public DbSet<LearnTag> Tags { get; set; }
        public DbSet<LearnProgressState> ProgressStates { get; set; }
        public DbSet<LearnLifecycleState> LifecycleStates { get; set; }
        public DbSet<LearnModuleTag> ModuleTags { get; set; }
        public DbSet<LearnNode> ContentItems { get; set; }
        public DbSet<LearnLesson> Lessons { get; set; }
        public DbSet<LearnLessonType> LessonTypes { get; set; }
        public DbSet<UserModuleProgress> UserModuleProgresses { get; set; }
        public DbSet<UserFinishedLesson> UserFinishedLessons { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurations
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserProfileConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
            modelBuilder.ApplyConfiguration(new PermissionConfiguration());
            modelBuilder.ApplyConfiguration(new RolePermissionConfiguration());
            modelBuilder.ApplyConfiguration(new ExternalLoginConfiguration());
            modelBuilder.ApplyConfiguration(new UserEffectivePermissionConfiguration());
            modelBuilder.ApplyConfiguration(new UserPermissionConfiguration());

            modelBuilder.ApplyConfiguration(new LearnModuleConfiguration());
            modelBuilder.ApplyConfiguration(new LearnCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new LearnTagConfiguration());
            modelBuilder.ApplyConfiguration(new LearnProgressStateConfiguration());
            modelBuilder.ApplyConfiguration(new LearnLifecycleStateConfiguration());
            modelBuilder.ApplyConfiguration(new LearnModuleTagConfiguration());
            modelBuilder.ApplyConfiguration(new LearnNodeConfiguration());
            modelBuilder.ApplyConfiguration(new LearnLessonConfiguration());
            modelBuilder.ApplyConfiguration(new LearnLessonTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UserModuleProgressConfiguration());
            modelBuilder.ApplyConfiguration(new UserFinishedLessonConfiguration());

            //DataSeed
            var allPermissions = new List<Permission>();
            modelBuilder.ApplyConfiguration(new UserSeedData(_configuration));
            modelBuilder.ApplyConfiguration(new RoleSeedData());
            modelBuilder.ApplyConfiguration(new UserRoleSeedData());
            modelBuilder.ApplyConfiguration(new PermissionSeedData(ref allPermissions));
            modelBuilder.ApplyConfiguration(new RolePermissionSeedData(allPermissions));
            modelBuilder.ApplyConfiguration(new CategorySeedData());
            modelBuilder.ApplyConfiguration(new LifecycleStateSeedData());
            modelBuilder.ApplyConfiguration(new ProgressStateSeedData());
            modelBuilder.ApplyConfiguration(new LessonTypeSeedData());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSnakeCaseNamingConvention();
        }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.State == EntityState.Modified)
                {
                    entry.Property("UpdatedAt").CurrentValue = DateTime.UtcNow;
                }
            }
            return base.SaveChanges();
        }

        //public override Task<int> SaveChangesAsync()
        //{
        //    foreach (var entry in ChangeTracker.Entries())
        //    {
        //        if (entry.State == EntityState.Modified)
        //        {
        //            entry.Property("UpdatedAt").CurrentValue = DateTime.UtcNow;
        //        }
        //    }
        //    this.SaveChangesAsync
        //    return base.SaveChangesAsync();
        //}

        
    }
}
