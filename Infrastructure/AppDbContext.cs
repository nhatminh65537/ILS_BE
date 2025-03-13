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

        public string SetupUserEffectivePermissionsQuery()
        {
            var query = @"
                CREATE MATERIALIZED VIEW user_effective_permissions AS
                SELECT
                    ur.user_id AS user_id,
                    rp.permission_id AS permission_id
                FROM user_roles ur
                JOIN role_permissions rp ON ur.role_id = rp.role_id
                UNION
                SELECT
                    up.user_id AS user_id,
                    up.permission_id AS permission_id
                FROM user_permissions up;

                CREATE INDEX ix_user_permission_id ON user_effective_permissions (user_id, permission_id);

                CREATE OR REPLACE FUNCTION refresh_user_effective_permissions() RETURNS TRIGGER AS $$
                BEGIN
                    REFRESH MATERIALIZED VIEW user_effective_permissions;
                    RETURN NULL;
                END;
                $$ LANGUAGE plpgsql;

                CREATE TRIGGER refresh_user_effective_permissions_trigger
                AFTER INSERT OR UPDATE OR DELETE ON user_roles
                FOR EACH STATEMENT
                EXECUTE FUNCTION refresh_user_effective_permissions();

                CREATE TRIGGER refresh_user_effective_permissions_trigger
                AFTER INSERT OR UPDATE OR DELETE ON role_permissions
                FOR EACH STATEMENT
                EXECUTE FUNCTION refresh_user_effective_permissions();

                CREATE TRIGGER refresh_user_effective_permissions_trigger
                AFTER INSERT OR UPDATE OR DELETE ON user_permissions
                FOR EACH STATEMENT
                EXECUTE FUNCTION refresh_user_effective_permissions();
            ";
            return query;
        }

        public string CreateTriggerCreateUserProfileQuery()
        {
            var query = @"
                CREATE OR REPLACE FUNCTION create_user_profile()
                RETURNS TRIGGER AS $$
                BEGIN
                    INSERT INTO user_profiles (id, display_name)
                    VALUES (NEW.id, NEW.user_name);
                    RETURN NEW;
                END;
                $$ LANGUAGE plpgsql;

                CREATE TRIGGER create_user_profile_trigger
                AFTER INSERT ON users
                FOR EACH ROW
                EXECUTE FUNCTION create_user_profile();
            ";
            return query;
        }

        public string CreateTriggerUpdateModuleOnLessonChangeQuery()
        {
            var query = @"
                CREATE OR REPLACE FUNCTION update_module_on_lesson_change()
                RETURNS TRIGGER AS $$
                BEGIN
                    UPDATE modules
                    SET xp = (SELECT COALESCE(SUM(xp), 0) FROM lessons WHERE content_item_id IN (SELECT id FROM content_items WHERE module_id = NEW.module_id)),
                        duration = (SELECT COALESCE(SUM(duration), 0) FROM lessons WHERE content_item_id IN (SELECT id FROM content_items WHERE module_id = NEW.module_id)),
                        updated_at = NOW()
                    WHERE id = (SELECT module_id FROM content_items WHERE id = NEW.content_item_id);
                    RETURN NEW;
                END;
                $$ LANGUAGE plpgsql;

                CREATE TRIGGER update_module_on_lesson_change_trigger
                AFTER INSERT OR UPDATE OR DELETE ON lessons
                FOR EACH ROW
                EXECUTE FUNCTION update_module_on_lesson_change();
            ";
            return query;
        }

        public string CreateTriggerUpdateModuleOnContentItemChangeQuery()
        {
            var query = @"
                CREATE OR REPLACE FUNCTION update_module_on_content_item_change()
                RETURNS TRIGGER AS $$
                BEGIN
                    UPDATE modules
                    SET xp = (SELECT COALESCE(SUM(xp), 0) FROM lessons WHERE content_item_id IN (SELECT id FROM content_items WHERE module_id = NEW.module_id)),
                        duration = (SELECT COALESCE(SUM(duration), 0) FROM lessons WHERE content_item_id IN (SELECT id FROM content_items WHERE module_id = NEW.module_id)),
                        updated_at = NOW()
                    WHERE id = NEW.module_id;
                    RETURN NEW;
                END;
                $$ LANGUAGE plpgsql;
                CREATE TRIGGER update_module_on_content_item_change_trigger
                AFTER INSERT OR UPDATE OR DELETE ON content_items
                FOR EACH ROW
                EXECUTE FUNCTION update_module_on_content_item_change();
            ";
            return query;
        }

        public string CreateTriggerUpdateUserProfileOnLessonFinishQuery()
        {
            var query = @"
                CREATE OR REPLACE FUNCTION update_user_profile_on_lesson_finish()
                RETURNS TRIGGER AS $$
                BEGIN
                    UPDATE user_profiles
                    SET xp = xp + NEW.xp
                    WHERE id = NEW.user_id;
                    RETURN NEW;
                END;
                $$ LANGUAGE plpgsql;
                CREATE TRIGGER update_user_profile_on_lesson_finish_trigger
                AFTER INSERT ON user_finished_lessons
                FOR EACH ROW
                EXECUTE FUNCTION update_user_profile_on_lesson_finish();
            ";
            return query;
        }
    }
}
