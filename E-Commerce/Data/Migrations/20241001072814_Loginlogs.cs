using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace E_Commerce.Data.Migrations
{
    /// <inheritdoc />
    public partial class Loginlogs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "39ae4fe9-4599-4cd0-bffa-9625058dce7c");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6c369e76-afbd-4802-a72a-559529271c20");

            migrationBuilder.CreateTable(
                name: "LoginLogs",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ip = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    loggedin = table.Column<int>(type: "int", nullable: false),
                    message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    pre_ids = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginLogs", x => x.id);
                    table.ForeignKey(
                        name: "FK_LoginLogs_AspNetUsers_user_id",
                        column: x => x.user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "IsAdmin", "IsLocked", "LastLogin", "LockoutEnabled", "LockoutEnd", "MemberSince", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "address", "created", "created_by", "dob", "dp", "gender_id", "modified", "modified_by", "nationality_id", "remarks", "status" },
                values: new object[,]
                {
                    { "1da7364d-fc6b-4e3e-b290-56727fec2c65", 0, "276362ce-72e3-4fef-a19e-54ff316295b4", "alamadcs@gmail.com", true, "Alam", false, false, null, false, null, new DateTime(2024, 10, 1, 11, 28, 13, 56, DateTimeKind.Local).AddTicks(1774), "ALAMADCS@GMAIL.COM", "ALAMADCS@GMAIL.COM", "AQAAAAIAAYagAAAAEAgwzx5s+AhmTjSDN9xXrHZDKNl8YG8xao+5jUGw9tyTc+hDJX/R73bgdgTtRpEqng==", "+121111111111", true, "014b6990-e713-414c-86d4-8467adc71061", false, "alamadcs@gmail.com", "UAE", new DateTime(2024, 9, 29, 19, 45, 33, 0, DateTimeKind.Unspecified), 1, new DateOnly(1991, 9, 29), "user2.png", 1, null, null, 1, null, 1 },
                    { "66feb4e1-2f1a-42e1-b5dc-7a12775b4d61", 0, "6ab77101-b4bc-4dc9-9986-cc508e30d30d", "alamnaryab@gmail.com", true, "Fakhre Alam", true, false, null, false, null, new DateTime(2024, 10, 1, 11, 28, 13, 56, DateTimeKind.Local).AddTicks(1704), "ALAMNARYAB@GMAIL.COM", "ALAMNARYAB@GMAIL.COM", "AQAAAAIAAYagAAAAEAgwzx5s+AhmTjSDN9xXrHZDKNl8YG8xao+5jUGw9tyTc+hDJX/R73bgdgTtRpEqng==", "+111111111111", true, "04d6b296-74a4-4b6b-ac6d-3a693cd8ccb1", false, "alamnaryab@gmail.com", "UAE", new DateTime(2024, 9, 29, 19, 45, 33, 0, DateTimeKind.Unspecified), 1, new DateOnly(1992, 9, 29), "user1.png", 1, null, null, 1, null, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_LoginLogs_user_id",
                table: "LoginLogs",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LoginLogs");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1da7364d-fc6b-4e3e-b290-56727fec2c65");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66feb4e1-2f1a-42e1-b5dc-7a12775b4d61");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "IsAdmin", "IsLocked", "LastLogin", "LockoutEnabled", "LockoutEnd", "MemberSince", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "address", "created", "created_by", "dob", "dp", "gender_id", "modified", "modified_by", "nationality_id", "remarks", "status" },
                values: new object[,]
                {
                    { "39ae4fe9-4599-4cd0-bffa-9625058dce7c", 0, "6ab77101-b4bc-4dc9-9986-cc508e30d30d", "alamnaryab@gmail.com", true, "Fakhre Alam", true, false, null, false, null, new DateTime(2024, 10, 1, 9, 1, 8, 242, DateTimeKind.Local).AddTicks(8326), "ALAMNARYAB@GMAIL.COM", "ALAMNARYAB@GMAIL.COM", "AQAAAAIAAYagAAAAEAgwzx5s+AhmTjSDN9xXrHZDKNl8YG8xao+5jUGw9tyTc+hDJX/R73bgdgTtRpEqng==", "+111111111111", true, "04d6b296-74a4-4b6b-ac6d-3a693cd8ccb1", false, "alamnaryab@gmail.com", "UAE", new DateTime(2024, 9, 29, 19, 45, 33, 0, DateTimeKind.Unspecified), 1, new DateOnly(1992, 9, 29), "user1.png", 1, null, null, 1, null, 1 },
                    { "6c369e76-afbd-4802-a72a-559529271c20", 0, "276362ce-72e3-4fef-a19e-54ff316295b4", "alamadcs@gmail.com", true, "Alam", false, false, null, false, null, new DateTime(2024, 10, 1, 9, 1, 8, 242, DateTimeKind.Local).AddTicks(8420), "ALAMADCS@GMAIL.COM", "ALAMADCS@GMAIL.COM", "AQAAAAIAAYagAAAAEAgwzx5s+AhmTjSDN9xXrHZDKNl8YG8xao+5jUGw9tyTc+hDJX/R73bgdgTtRpEqng==", "+121111111111", true, "014b6990-e713-414c-86d4-8467adc71061", false, "alamadcs@gmail.com", "UAE", new DateTime(2024, 9, 29, 19, 45, 33, 0, DateTimeKind.Unspecified), 1, new DateOnly(1991, 9, 29), "user2.png", 1, null, null, 1, null, 1 }
                });
        }
    }
}
