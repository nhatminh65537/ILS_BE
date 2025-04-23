using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ILS_BE.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModuleConstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_modules_categories_category_id",
                table: "modules");

            migrationBuilder.DropForeignKey(
                name: "fk_modules_lifecycle_states_lifecycle_state_id",
                table: "modules");

            migrationBuilder.AddForeignKey(
                name: "fk_modules_categories_category_id",
                table: "modules",
                column: "category_id",
                principalTable: "categories",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_modules_lifecycle_states_lifecycle_state_id",
                table: "modules",
                column: "lifecycle_state_id",
                principalTable: "lifecycle_states",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_modules_categories_category_id",
                table: "modules");

            migrationBuilder.DropForeignKey(
                name: "fk_modules_lifecycle_states_lifecycle_state_id",
                table: "modules");

            migrationBuilder.AddForeignKey(
                name: "fk_modules_categories_category_id",
                table: "modules",
                column: "category_id",
                principalTable: "categories",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_modules_lifecycle_states_lifecycle_state_id",
                table: "modules",
                column: "lifecycle_state_id",
                principalTable: "lifecycle_states",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
