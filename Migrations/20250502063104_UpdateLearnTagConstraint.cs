using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ILS_BE.Migrations
{
    /// <inheritdoc />
    public partial class UpdateLearnTagConstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_learn_tags_learn_modules_learn_module_id",
                table: "learn_tags");

            migrationBuilder.DropIndex(
                name: "ix_learn_tags_learn_module_id",
                table: "learn_tags");

            migrationBuilder.DropColumn(
                name: "learn_module_id",
                table: "learn_tags");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "learn_module_id",
                table: "learn_tags",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_learn_tags_learn_module_id",
                table: "learn_tags",
                column: "learn_module_id");

            migrationBuilder.AddForeignKey(
                name: "fk_learn_tags_learn_modules_learn_module_id",
                table: "learn_tags",
                column: "learn_module_id",
                principalTable: "learn_modules",
                principalColumn: "id");
        }
    }
}
