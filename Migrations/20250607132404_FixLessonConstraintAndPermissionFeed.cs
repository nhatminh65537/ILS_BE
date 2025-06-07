using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ILS_BE.Migrations
{
    /// <inheritdoc />
    public partial class FixLessonConstraintAndPermissionFeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_learn_lessons_title",
                table: "learn_lessons");

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id",
                keyValue: 4,
                column: "name",
                value: "Auth.ChangePassword");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id",
                keyValue: 4,
                column: "name",
                value: "Auth.ResetPassword");

            migrationBuilder.CreateIndex(
                name: "ix_learn_lessons_title",
                table: "learn_lessons",
                column: "title",
                unique: true);
        }
    }
}
