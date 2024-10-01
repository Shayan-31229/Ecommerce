using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace E_Commerce.Data.Migrations
{
    /// <inheritdoc />
    public partial class firstMigrationByAlam : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "address",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "created",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "created_by",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateOnly>(
                name: "dob",
                table: "AspNetUsers",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "dp",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "gender_id",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "modified",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "modified_by",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "nationality_id",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "remarks",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "status",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Genders",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    sort = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    created_by = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    modified_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genders", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Nationalities",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    sort = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    created_by = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    modified_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nationalities", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    group = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    sort = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    created_by = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    modified_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "Genders",
                columns: new[] { "id", "created", "created_by", "modified", "modified_by", "sort", "status", "title" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 9, 29, 19, 45, 33, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000001"), null, null, 1, 1, "Male" },
                    { 2, new DateTime(2024, 9, 29, 19, 45, 33, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000001"), null, null, 2, 1, "Female" }
                });

            migrationBuilder.InsertData(
                table: "Nationalities",
                columns: new[] { "id", "created", "created_by", "modified", "modified_by", "sort", "status", "title" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 9, 29, 19, 45, 33, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000001"), null, null, 1, 1, "UAE" },
                    { 2, new DateTime(2024, 9, 29, 19, 45, 33, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000001"), null, null, 2, 1, "PAK" }
                });

            migrationBuilder.InsertData(
                table: "Settings",
                columns: new[] { "id", "created", "created_by", "group", "modified", "modified_by", "sort", "status", "title", "value" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 9, 29, 19, 45, 33, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000001"), null, null, null, 1, 1, "app_name", "AB ECommerce" },
                    { 2, new DateTime(2024, 9, 29, 19, 45, 33, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000001"), null, null, null, 2, 1, "logo", "logo.png" },
                    { 3, new DateTime(2024, 9, 29, 19, 45, 33, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000001"), null, null, null, 2, 1, "currency", "PKR" },
                    { 4, new DateTime(2024, 9, 29, 19, 45, 33, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000001"), null, null, null, 2, 1, "contact_number", "03335662558" },
                    { 5, new DateTime(2024, 9, 29, 19, 45, 33, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000001"), null, null, null, 2, 1, "contact_email", "support@codingsips.com" },
                    { 6, new DateTime(2024, 9, 29, 19, 45, 33, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000001"), null, null, null, 2, 1, "max_upload_size_in_mbs", "5" },
                    { 7, new DateTime(2024, 9, 29, 19, 45, 33, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000001"), null, null, null, 2, 1, "allowed_upload_extensions", ".jpg,.jpeg,.png,.pdf,.docx,.xlsx,.txt" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "IsAdmin", "IsLocked", "LastLogin", "LockoutEnabled", "LockoutEnd", "MemberSince", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "address", "created", "created_by", "dob", "dp", "gender_id", "modified", "modified_by", "nationality_id", "remarks", "status" },
                values: new object[,]
                {
                    { "39ae4fe9-4599-4cd0-bffa-9625058dce7c", 0, "6ab77101-b4bc-4dc9-9986-cc508e30d30d", "alamnaryab@gmail.com", true, "Fakhre Alam", true, false, null, false, null, new DateTime(2024, 10, 1, 9, 1, 8, 242, DateTimeKind.Local).AddTicks(8326), "ALAMNARYAB@GMAIL.COM", "ALAMNARYAB@GMAIL.COM", "AQAAAAIAAYagAAAAEAgwzx5s+AhmTjSDN9xXrHZDKNl8YG8xao+5jUGw9tyTc+hDJX/R73bgdgTtRpEqng==", "+111111111111", true, "04d6b296-74a4-4b6b-ac6d-3a693cd8ccb1", false, "alamnaryab@gmail.com", "UAE", new DateTime(2024, 9, 29, 19, 45, 33, 0, DateTimeKind.Unspecified), 1, new DateOnly(1992, 9, 29), "user1.png", 1, null, null, 1, null, 1 },
                    { "6c369e76-afbd-4802-a72a-559529271c20", 0, "276362ce-72e3-4fef-a19e-54ff316295b4", "alamadcs@gmail.com", true, "Alam", false, false, null, false, null, new DateTime(2024, 10, 1, 9, 1, 8, 242, DateTimeKind.Local).AddTicks(8420), "ALAMADCS@GMAIL.COM", "ALAMADCS@GMAIL.COM", "AQAAAAIAAYagAAAAEAgwzx5s+AhmTjSDN9xXrHZDKNl8YG8xao+5jUGw9tyTc+hDJX/R73bgdgTtRpEqng==", "+121111111111", true, "014b6990-e713-414c-86d4-8467adc71061", false, "alamadcs@gmail.com", "UAE", new DateTime(2024, 9, 29, 19, 45, 33, 0, DateTimeKind.Unspecified), 1, new DateOnly(1991, 9, 29), "user2.png", 1, null, null, 1, null, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_gender_id",
                table: "AspNetUsers",
                column: "gender_id");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_nationality_id",
                table: "AspNetUsers",
                column: "nationality_id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Genders_gender_id",
                table: "AspNetUsers",
                column: "gender_id",
                principalTable: "Genders",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Nationalities_nationality_id",
                table: "AspNetUsers",
                column: "nationality_id",
                principalTable: "Nationalities",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Genders_gender_id",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Nationalities_nationality_id",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Genders");

            migrationBuilder.DropTable(
                name: "Nationalities");

            migrationBuilder.DropTable(
                name: "Settings");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_gender_id",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_nationality_id",
                table: "AspNetUsers");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "39ae4fe9-4599-4cd0-bffa-9625058dce7c");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6c369e76-afbd-4802-a72a-559529271c20");

            migrationBuilder.DropColumn(
                name: "address",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "created",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "created_by",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "dob",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "dp",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "gender_id",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "modified",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "modified_by",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "nationality_id",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "remarks",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "status",
                table: "AspNetUsers");
        }
    }
}
