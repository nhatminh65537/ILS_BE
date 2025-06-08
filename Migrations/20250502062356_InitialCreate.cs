using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ILS_BE.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "learn_categories",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_learn_categories", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "learn_lesson_types",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_learn_lesson_types", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "learn_lifecycle_states",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_learn_lifecycle_states", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "learn_progress_states",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_learn_progress_states", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "permissions",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_permissions", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    changeable = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_name = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    email_verified = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    phone_number = table.Column<string>(type: "text", nullable: true),
                    password_hash = table.Column<string>(type: "text", nullable: false),
                    require_password_reset = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "learn_lessons",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "text", nullable: false),
                    type_id = table.Column<int>(type: "integer", nullable: false),
                    content = table.Column<string>(type: "text", nullable: false),
                    xp = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    duration = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_learn_lessons", x => x.id);
                    table.ForeignKey(
                        name: "fk_learn_lessons_learn_lesson_types_type_id",
                        column: x => x.type_id,
                        principalTable: "learn_lesson_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "role_permissions",
                columns: table => new
                {
                    role_id = table.Column<int>(type: "integer", nullable: false),
                    permission_id = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_role_permissions", x => new { x.role_id, x.permission_id });
                    table.ForeignKey(
                        name: "fk_role_permissions_permissions_permission_id",
                        column: x => x.permission_id,
                        principalTable: "permissions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_role_permissions_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "external_logins",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    provider = table.Column<string>(type: "text", nullable: false),
                    provider_user_id = table.Column<string>(type: "text", nullable: false),
                    provider_data = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_external_logins", x => new { x.user_id, x.provider });
                    table.ForeignKey(
                        name: "fk_external_logins_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_permissions",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    permission_id = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_permissions", x => new { x.user_id, x.permission_id });
                    table.ForeignKey(
                        name: "fk_user_permissions_permissions_permission_id",
                        column: x => x.permission_id,
                        principalTable: "permissions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_permissions_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_profiles",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    first_name = table.Column<string>(type: "text", nullable: true),
                    last_name = table.Column<string>(type: "text", nullable: true),
                    display_name = table.Column<string>(type: "text", nullable: false),
                    xp = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    level = table.Column<int>(type: "integer", nullable: false, defaultValue: 1),
                    avatar_path = table.Column<string>(type: "text", nullable: true),
                    bio = table.Column<string>(type: "text", nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_profiles", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_profiles_users_id",
                        column: x => x.id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_roles",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    role_id = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_roles", x => new { x.user_id, x.role_id });
                    table.ForeignKey(
                        name: "fk_user_roles_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_roles_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "learn_nodes",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "text", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    is_lesson = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    lesson_id = table.Column<int>(type: "integer", nullable: true),
                    order = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    path = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_learn_nodes", x => x.id);
                    table.ForeignKey(
                        name: "fk_learn_nodes_learn_lessons_lesson_id",
                        column: x => x.lesson_id,
                        principalTable: "learn_lessons",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_finished_lessons",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    lesson_id = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_finished_lessons", x => new { x.user_id, x.lesson_id });
                    table.ForeignKey(
                        name: "fk_user_finished_lessons_learn_lessons_lesson_id",
                        column: x => x.lesson_id,
                        principalTable: "learn_lessons",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_finished_lessons_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "learn_modules",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    node_id = table.Column<int>(type: "integer", nullable: false),
                    title = table.Column<string>(type: "text", nullable: false),
                    image_path = table.Column<string>(type: "text", nullable: true),
                    created_by = table.Column<int>(type: "integer", nullable: true),
                    description = table.Column<string>(type: "text", nullable: false),
                    category_id = table.Column<int>(type: "integer", nullable: false),
                    lifecycle_state_id = table.Column<int>(type: "integer", nullable: false),
                    xp = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    duration = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_learn_modules", x => x.id);
                    table.ForeignKey(
                        name: "fk_learn_modules_learn_categories_category_id",
                        column: x => x.category_id,
                        principalTable: "learn_categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_learn_modules_learn_lifecycle_states_lifecycle_state_id",
                        column: x => x.lifecycle_state_id,
                        principalTable: "learn_lifecycle_states",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_learn_modules_learn_nodes_node_id",
                        column: x => x.node_id,
                        principalTable: "learn_nodes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_learn_modules_users_created_by",
                        column: x => x.created_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "learn_tags",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    learn_module_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_learn_tags", x => x.id);
                    table.ForeignKey(
                        name: "fk_learn_tags_learn_modules_learn_module_id",
                        column: x => x.learn_module_id,
                        principalTable: "learn_modules",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "user_module_progresses",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    module_id = table.Column<int>(type: "integer", nullable: false),
                    progress_state_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_module_progresses", x => new { x.user_id, x.module_id });
                    table.ForeignKey(
                        name: "fk_user_module_progresses_learn_modules_module_id",
                        column: x => x.module_id,
                        principalTable: "learn_modules",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_module_progresses_learn_progress_states_progress_state",
                        column: x => x.progress_state_id,
                        principalTable: "learn_progress_states",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_module_progresses_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "learn_module_tags",
                columns: table => new
                {
                    module_id = table.Column<int>(type: "integer", nullable: false),
                    tag_id = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_learn_module_tags", x => new { x.module_id, x.tag_id });
                    table.ForeignKey(
                        name: "fk_learn_module_tags_learn_modules_module_id",
                        column: x => x.module_id,
                        principalTable: "learn_modules",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_learn_module_tags_learn_tags_tag_id",
                        column: x => x.tag_id,
                        principalTable: "learn_tags",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "learn_categories",
                columns: new[] { "id", "description", "name" },
                values: new object[,]
                {
                    { 1, "Crypto related modules", "Crypto" },
                    { 2, "Pwn related modules", "Pwn" },
                    { 3, "Rev related modules", "Rev" },
                    { 4, "Web related modules", "Web" }
                });

            migrationBuilder.InsertData(
                table: "learn_lesson_types",
                columns: new[] { "id", "description", "name" },
                values: new object[,]
                {
                    { 1, "Markdown lesson", "Markdown" },
                    { 2, "Video lesson", "Video" },
                    { 3, "Quiz lesson", "Quiz" }
                });

            migrationBuilder.InsertData(
                table: "learn_lifecycle_states",
                columns: new[] { "id", "description", "name" },
                values: new object[,]
                {
                    { 1, "Module is being created", "Creating" },
                    { 2, "Module is being updated", "Updating" },
                    { 3, "Module is published", "Published" }
                });

            migrationBuilder.InsertData(
                table: "learn_progress_states",
                columns: new[] { "id", "description", "name" },
                values: new object[,]
                {
                    { 1, "Module is not started", "Locked" },
                    { 2, "Module is being learned", "Learning" },
                    { 3, "Module is completed", "Completed" }
                });

            migrationBuilder.InsertData(
                table: "permissions",
                columns: new[] { "id", "description", "name" },
                values: new object[,]
                {
                    { 1, null, "Auth.Login" },
                    { 2, null, "Auth.Register" },
                    { 3, null, "Auth.Logout" },
                    { 4, null, "Auth.ResetPassword" },
                    { 5, null, "LearnCategories.GetAll" },
                    { 6, null, "LearnCategories.Get" },
                    { 7, null, "LearnCategories.Create" },
                    { 8, null, "LearnCategories.Update" },
                    { 9, null, "LearnCategories.Delete" },
                    { 10, null, "LearnLessons.GetAll" },
                    { 11, null, "LearnLessons.Get" },
                    { 12, null, "LearnLessons.Create" },
                    { 13, null, "LearnLessons.Update" },
                    { 14, null, "LearnLessons.Delete" },
                    { 15, null, "LearnLessonTypes.GetAll" },
                    { 16, null, "LearnLessonTypes.Get" },
                    { 17, null, "LearnLessonTypes.Update" },
                    { 18, null, "LearnLifecycleStates.GetAll" },
                    { 19, null, "LearnLifecycleStates.Get" },
                    { 20, null, "LearnLifecycleStates.Put" },
                    { 21, null, "LearnModules.GetAll" },
                    { 22, null, "LearnModules.Get" },
                    { 23, null, "LearnModules.Create" },
                    { 24, null, "LearnModules.Update" },
                    { 25, null, "LearnModules.Delete" },
                    { 26, null, "LearnNodes.GetAll" },
                    { 27, null, "LearnNodes.Get" },
                    { 28, null, "LearnNodes.Create" },
                    { 29, null, "LearnNodes.Update" },
                    { 30, null, "LearnNodes.Delete" },
                    { 31, null, "LearnNodes.GetTree" },
                    { 32, null, "LearnNodes.UpdateTree" },
                    { 33, null, "LearnProgressStates.GetAll" },
                    { 34, null, "LearnProgressStates.Get" },
                    { 35, null, "LearnProgressStates.Update" },
                    { 36, null, "LearnTags.GetAll" },
                    { 37, null, "LearnTags.Get" },
                    { 38, null, "LearnTags.Create" },
                    { 39, null, "LearnTags.Update" },
                    { 40, null, "LearnTags.Delete" },
                    { 41, null, "MyUser.Get" },
                    { 42, null, "MyUser.Update" },
                    { 43, null, "MyUser.GetPermissions" },
                    { 44, null, "MyUser.GetRoles" },
                    { 45, null, "MyUser.GetProfile" },
                    { 46, null, "Permissions.GetAll" },
                    { 47, null, "Permissions.Get" },
                    { 48, null, "Permissions.Put" },
                    { 49, null, "Roles.GetAll" },
                    { 50, null, "Roles.Get" },
                    { 51, null, "Roles.Create" },
                    { 52, null, "Roles.Update" },
                    { 53, null, "Roles.Delete" },
                    { 54, null, "Roles.GetPermissions" },
                    { 55, null, "Roles.AddPermission" },
                    { 56, null, "Roles.RemovePermission" },
                    { 57, null, "Users.GetAll" },
                    { 58, null, "Users.Get" },
                    { 59, null, "Users.Create" },
                    { 60, null, "Users.Update" },
                    { 61, null, "Users.Delete" },
                    { 62, null, "Users.GetProfile" },
                    { 63, null, "Users.GetPermissions" },
                    { 64, null, "Users.AddPermission" },
                    { 65, null, "Users.RemovePermission" },
                    { 66, null, "Users.GetRoles" },
                    { 67, null, "Users.AddRole" },
                    { 68, null, "Users.RemoveRole" }
                });

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "id", "description", "name", "changeable" },
                values: new object[,]
                {
                    { 1, null, "Admin", false },
                    { 2, null, "Collaborator", false },
                    { 3, null, "User", false }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "email", "password_hash", "phone_number", "user_name" },
                values: new object[] { 1, "admin@example.com", "AQAAAAIAAYagAAAAEHjE3hY5B8vL0+2kQFwd+ASKpRjfOV255rUBrem4+2nYCm7NK8X7ZFbLr3JBaW/AYQ==", null, "admin" });

            migrationBuilder.InsertData(
                table: "role_permissions",
                columns: new[] { "permission_id", "role_id" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 3, 1 },
                    { 4, 1 },
                    { 5, 1 },
                    { 6, 1 },
                    { 7, 1 },
                    { 8, 1 },
                    { 9, 1 },
                    { 10, 1 },
                    { 11, 1 },
                    { 12, 1 },
                    { 13, 1 },
                    { 14, 1 },
                    { 15, 1 },
                    { 16, 1 },
                    { 17, 1 },
                    { 18, 1 },
                    { 19, 1 },
                    { 20, 1 },
                    { 21, 1 },
                    { 22, 1 },
                    { 23, 1 },
                    { 24, 1 },
                    { 25, 1 },
                    { 26, 1 },
                    { 27, 1 },
                    { 28, 1 },
                    { 29, 1 },
                    { 30, 1 },
                    { 31, 1 },
                    { 32, 1 },
                    { 33, 1 },
                    { 34, 1 },
                    { 35, 1 },
                    { 36, 1 },
                    { 37, 1 },
                    { 38, 1 },
                    { 39, 1 },
                    { 40, 1 },
                    { 41, 1 },
                    { 42, 1 },
                    { 43, 1 },
                    { 44, 1 },
                    { 45, 1 },
                    { 46, 1 },
                    { 47, 1 },
                    { 48, 1 },
                    { 49, 1 },
                    { 50, 1 },
                    { 51, 1 },
                    { 52, 1 },
                    { 53, 1 },
                    { 54, 1 },
                    { 55, 1 },
                    { 56, 1 },
                    { 57, 1 },
                    { 58, 1 },
                    { 59, 1 },
                    { 60, 1 },
                    { 61, 1 },
                    { 62, 1 },
                    { 63, 1 },
                    { 64, 1 },
                    { 65, 1 },
                    { 66, 1 },
                    { 67, 1 },
                    { 68, 1 }
                });

            migrationBuilder.InsertData(
                table: "user_roles",
                columns: new[] { "role_id", "user_id" },
                values: new object[] { 1, 1 });

            migrationBuilder.CreateIndex(
                name: "ix_external_logins_provider_user_id",
                table: "external_logins",
                column: "provider_user_id");

            migrationBuilder.CreateIndex(
                name: "ix_learn_categories_name",
                table: "learn_categories",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_learn_lesson_types_name",
                table: "learn_lesson_types",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_learn_lessons_title",
                table: "learn_lessons",
                column: "title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_learn_lessons_type_id",
                table: "learn_lessons",
                column: "type_id");

            migrationBuilder.CreateIndex(
                name: "ix_learn_lifecycle_states_name",
                table: "learn_lifecycle_states",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_learn_module_tags_module_id",
                table: "learn_module_tags",
                column: "module_id");

            migrationBuilder.CreateIndex(
                name: "ix_learn_module_tags_tag_id",
                table: "learn_module_tags",
                column: "tag_id");

            migrationBuilder.CreateIndex(
                name: "ix_learn_modules_category_id",
                table: "learn_modules",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "ix_learn_modules_created_by",
                table: "learn_modules",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "ix_learn_modules_lifecycle_state_id",
                table: "learn_modules",
                column: "lifecycle_state_id");

            migrationBuilder.CreateIndex(
                name: "ix_learn_modules_node_id",
                table: "learn_modules",
                column: "node_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_learn_modules_title",
                table: "learn_modules",
                column: "title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_learn_nodes_lesson_id",
                table: "learn_nodes",
                column: "lesson_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_learn_nodes_title",
                table: "learn_nodes",
                column: "title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_learn_progress_states_name",
                table: "learn_progress_states",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_learn_tags_learn_module_id",
                table: "learn_tags",
                column: "learn_module_id");

            migrationBuilder.CreateIndex(
                name: "ix_learn_tags_name",
                table: "learn_tags",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_permissions_name",
                table: "permissions",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_role_permissions_permission_id",
                table: "role_permissions",
                column: "permission_id");

            migrationBuilder.CreateIndex(
                name: "ix_role_permissions_role_id",
                table: "role_permissions",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "ix_roles_name",
                table: "roles",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_user_finished_lessons_lesson_id",
                table: "user_finished_lessons",
                column: "lesson_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_finished_lessons_user_id",
                table: "user_finished_lessons",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_module_progresses_module_id",
                table: "user_module_progresses",
                column: "module_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_module_progresses_progress_state_id",
                table: "user_module_progresses",
                column: "progress_state_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_module_progresses_user_id",
                table: "user_module_progresses",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_permissions_permission_id",
                table: "user_permissions",
                column: "permission_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_permissions_user_id",
                table: "user_permissions",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_profiles_display_name",
                table: "user_profiles",
                column: "display_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_user_roles_role_id",
                table: "user_roles",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_roles_user_id",
                table: "user_roles",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_users_email",
                table: "users",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_users_phone_number",
                table: "users",
                column: "phone_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_users_user_name",
                table: "users",
                column: "user_name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "external_logins");

            migrationBuilder.DropTable(
                name: "learn_module_tags");

            migrationBuilder.DropTable(
                name: "role_permissions");

            migrationBuilder.DropTable(
                name: "user_finished_lessons");

            migrationBuilder.DropTable(
                name: "user_module_progresses");

            migrationBuilder.DropTable(
                name: "user_permissions");

            migrationBuilder.DropTable(
                name: "user_profiles");

            migrationBuilder.DropTable(
                name: "user_roles");

            migrationBuilder.DropTable(
                name: "learn_tags");

            migrationBuilder.DropTable(
                name: "learn_progress_states");

            migrationBuilder.DropTable(
                name: "permissions");

            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropTable(
                name: "learn_modules");

            migrationBuilder.DropTable(
                name: "learn_categories");

            migrationBuilder.DropTable(
                name: "learn_lifecycle_states");

            migrationBuilder.DropTable(
                name: "learn_nodes");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "learn_lessons");

            migrationBuilder.DropTable(
                name: "learn_lesson_types");
        }
    }
}
