using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ILS_BE.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTriggerAndView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Create user_permissions_view
            migrationBuilder.Sql(@"
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
                ");

            migrationBuilder.Sql(@"
                CREATE INDEX ix_user_permission_id ON user_effective_permissions (user_id, permission_id);
                ");

            // Trigger to refresh user_permissions_view
            migrationBuilder.Sql(@"
                CREATE OR REPLACE FUNCTION refresh_user_effective_permissions() RETURNS TRIGGER AS $$
                BEGIN
                    REFRESH MATERIALIZED VIEW user_effective_permissions;
                    RETURN NULL;
                END;
                $$ LANGUAGE plpgsql;
                ");

            migrationBuilder.Sql(@"
                CREATE TRIGGER refresh_user_effective_permissions_trigger
                AFTER INSERT OR UPDATE OR DELETE ON user_roles
                FOR EACH STATEMENT
                EXECUTE FUNCTION refresh_user_effective_permissions();
                ");

            migrationBuilder.Sql(@"
                CREATE TRIGGER refresh_user_effective_permissions_trigger
                AFTER INSERT OR UPDATE OR DELETE ON role_permissions
                FOR EACH STATEMENT
                EXECUTE FUNCTION refresh_user_effective_permissions();
                ");

            migrationBuilder.Sql(@"
                CREATE TRIGGER refresh_user_effective_permissions_trigger
                AFTER INSERT OR UPDATE OR DELETE ON user_permissions
                FOR EACH STATEMENT
                EXECUTE FUNCTION refresh_user_effective_permissions();
                ");

            // Trigger to create user profile
            migrationBuilder.Sql(@"
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
            ");

            // Trigger to update modules when lessons are updated
            migrationBuilder.Sql(@"
                CREATE OR REPLACE FUNCTION update_module_on_lesson_change()
                RETURNS TRIGGER AS $$
                DECLARE
                    module_ci_id integer;
                BEGIN
                    -- Extract the module content_item id from the lesson's content_item path.
                    -- The pattern '\.(\d+)\.' captures the first number between dots.
                    SELECT substring(path FROM '\.(\d+)\.')::integer
                      INTO module_ci_id
                      FROM content_items
                     WHERE id = NEW.content_item_id;
    
                    -- Update the module record by summing xp and duration from all lessons
                    -- whose content_item's path starts with the module's content_item id.
                    UPDATE modules
                    SET xp = (
                            SELECT COALESCE(SUM(l.xp), 0)
                              FROM lessons l
                              JOIN content_items ci ON l.content_item_id = ci.id
                             WHERE ci.path LIKE '.' || module_ci_id::text || '.%'
                          ),
                        duration = (
                            SELECT COALESCE(SUM(l.duration), 0)
                              FROM lessons l
                              JOIN content_items ci ON l.content_item_id = ci.id
                             WHERE ci.path LIKE '.' || module_ci_id::text || '.%'
                          ),
                        updated_at = NOW()
                    WHERE id = (
                            -- Get the module id from the module content item
                            SELECT module_id FROM content_items WHERE id = module_ci_id
                          );
    
                    RETURN NEW;
                END;
                $$ LANGUAGE plpgsql;


                CREATE TRIGGER update_module_on_lesson_change_trigger
                AFTER INSERT OR UPDATE OR DELETE ON lessons
                FOR EACH ROW
                EXECUTE FUNCTION update_module_on_lesson_change();
            ");

            // Trigger to update modules when content items are updated
            migrationBuilder.Sql(@"
                CREATE OR REPLACE FUNCTION update_module_on_content_item_change()
                RETURNS TRIGGER AS $$
                DECLARE
                    module_ci_id integer;
                BEGIN
                    module_ci_id := substring(NEW.path FROM '\.(\d+)\.')::integer;

                    UPDATE modules
                    SET updated_at = NOW()
                    WHERE id = module_ci_id;
                    
                    RETURN NEW;
                END;
                $$ LANGUAGE plpgsql;

                CREATE TRIGGER update_module_on_content_item_change_trigger
                AFTER INSERT OR UPDATE OR DELETE ON content_items
                FOR EACH ROW
                EXECUTE FUNCTION update_module_on_content_item_change();
            ");
            // Trigger to update user_profiles when a lesson is finished
            migrationBuilder.Sql(@"
                CREATE OR REPLACE FUNCTION update_user_profile_on_lesson_finish()
                RETURNS TRIGGER AS $$
                BEGIN
                    UPDATE user_profiles
                    SET xp = xp + (SELECT xp FROM lessons WHERE id = NEW.lesson_id),
                        duration = duration + (SELECT duration FROM lessons WHERE id = NEW.lesson_id),
                        updated_at = NOW()
                    WHERE user_id = NEW.user_id;
                    RETURN NEW;
                END;
                $$ LANGUAGE plpgsql;

                CREATE TRIGGER update_user_profile_on_lesson_finish_trigger
                AFTER INSERT ON user_finished_lessons
                FOR EACH ROW
                EXECUTE FUNCTION update_user_profile_on_lesson_finish();
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DROP TRIGGER IF EXISTS refresh_user_effective_permissions_trigger ON user_roles;
                DROP TRIGGER IF EXISTS refresh_user_effective_permissions_trigger ON role_permissions;
                DROP TRIGGER IF EXISTS refresh_user_effective_permissions_trigger ON user_permissions;
                DROP FUNCTION IF EXISTS refresh_user_effective_permissions;
                DROP MATERIALIZED VIEW IF EXISTS user_effective_permissions;
                ");
            migrationBuilder.Sql(@"
                DROP TRIGGER IF EXISTS create_user_profile_trigger ON users;
                DROP FUNCTION IF EXISTS create_user_profile();
            ");
            migrationBuilder.Sql(@"
                DROP TRIGGER IF EXISTS update_module_on_lesson_change_trigger ON lessons;
                DROP FUNCTION IF EXISTS update_module_on_lesson_change;
            ");

            migrationBuilder.Sql(@"
                DROP TRIGGER IF EXISTS update_module_on_content_item_change_trigger ON content_items;
                DROP FUNCTION IF EXISTS update_module_on_content_item_change;
            ");
            migrationBuilder.Sql(@"
                DROP TRIGGER IF EXISTS update_user_profile_on_lesson_finish_trigger ON user_finished_lesson;
                DROP FUNCTION IF EXISTS update_user_profile_on_lesson_finish;
            ");
        }
    }
}
