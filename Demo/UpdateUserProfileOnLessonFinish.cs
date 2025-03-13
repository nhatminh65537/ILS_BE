using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ILS_BE.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserProfileOnLessonFinish : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                AFTER INSERT ON user_finished_lesson
                FOR EACH ROW
                EXECUTE FUNCTION update_user_profile_on_lesson_finish();
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DROP TRIGGER IF EXISTS update_user_profile_on_lesson_finish_trigger ON user_finished_lesson;
                DROP FUNCTION IF EXISTS update_user_profile_on_lesson_finish;
            ");
        }
    }
}

