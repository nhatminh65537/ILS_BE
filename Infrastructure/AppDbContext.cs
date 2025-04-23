using ILS_BE.Domain.Models;
using Microsoft.EntityFrameworkCore;
using ILS_BE.Infrastructure.Configurations;
using ILS_BE.Infrastructure.SeedData;

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

        public DbSet<Module> Modules { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<ProgressState> ProgressStates { get; set; }
        public DbSet<LifecycleState> LifecycleStates { get; set; }
        public DbSet<ModuleTag> ModuleTags { get; set; }
        public DbSet<ContentItem> ContentItems { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<LessonType> LessonTypes { get; set; }
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

            modelBuilder.ApplyConfiguration(new ModuleConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new TagConfiguration());
            modelBuilder.ApplyConfiguration(new ProgressStateConfiguration());
            modelBuilder.ApplyConfiguration(new LifecycleStateConfiguration());
            modelBuilder.ApplyConfiguration(new ModuleTagConfiguration());
            modelBuilder.ApplyConfiguration(new ContentItemConfiguration());
            modelBuilder.ApplyConfiguration(new LessonConfiguration());
            modelBuilder.ApplyConfiguration(new LessonTypeConfiguration());
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

        
    }
}
