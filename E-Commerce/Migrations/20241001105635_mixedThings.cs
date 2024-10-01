using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace E_Commerce.Migrations
{
    /// <inheritdoc />
    public partial class mixedThings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    LastLogin = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MemberSince = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsLocked = table.Column<bool>(type: "bit", nullable: false),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false),
                    gender_id = table.Column<int>(type: "int", nullable: true),
                    nationality_id = table.Column<int>(type: "int", nullable: true),
                    dob = table.Column<DateOnly>(type: "date", nullable: true),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    status = table.Column<int>(type: "int", nullable: false),
                    remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    created_by = table.Column<int>(type: "int", nullable: false),
                    modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    modified_by = table.Column<int>(type: "int", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Genders_gender_id",
                        column: x => x.gender_id,
                        principalTable: "Genders",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Nationalities_nationality_id",
                        column: x => x.nationality_id,
                        principalTable: "Nationalities",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "12329f10-ef4e-4922-bb05-3e7a3ffdd125", null, "sa", "SA" },
                    { "e32c6f7a-3cf5-4ee0-9d1c-13e4af64d364", null, "can_view_users", "CAN_VIEW_USERS" }
                });

            migrationBuilder.InsertData(
                table: "Genders",
                columns: new[] { "id", "created", "created_by", "modified", "modified_by", "sort", "status", "title" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 9, 29, 19, 45, 33, 0, DateTimeKind.Unspecified), new Guid("9055afc0-351b-4dcd-80cf-11b1fb0a729b"), null, null, 1, 1, "Male" },
                    { 2, new DateTime(2024, 9, 29, 19, 45, 33, 0, DateTimeKind.Unspecified), new Guid("9055afc0-351b-4dcd-80cf-11b1fb0a729b"), null, null, 2, 1, "Female" }
                });

            migrationBuilder.InsertData(
                table: "Nationalities",
                columns: new[] { "id", "created", "created_by", "modified", "modified_by", "sort", "status", "title" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 9, 29, 19, 45, 33, 0, DateTimeKind.Unspecified), new Guid("9055afc0-351b-4dcd-80cf-11b1fb0a729b"), null, null, 1, 1, "UAE" },
                    { 2, new DateTime(2024, 9, 29, 19, 45, 33, 0, DateTimeKind.Unspecified), new Guid("9055afc0-351b-4dcd-80cf-11b1fb0a729b"), null, null, 2, 1, "PAK" }
                });

            migrationBuilder.InsertData(
                table: "Settings",
                columns: new[] { "id", "created", "created_by", "group", "modified", "modified_by", "sort", "status", "title", "value" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 9, 29, 19, 45, 33, 0, DateTimeKind.Unspecified), new Guid("9055afc0-351b-4dcd-80cf-11b1fb0a729b"), null, null, null, 1, 1, "app_name", "AB ECommerce" },
                    { 2, new DateTime(2024, 9, 29, 19, 45, 33, 0, DateTimeKind.Unspecified), new Guid("9055afc0-351b-4dcd-80cf-11b1fb0a729b"), null, null, null, 2, 1, "logo", "logo.png" },
                    { 3, new DateTime(2024, 9, 29, 19, 45, 33, 0, DateTimeKind.Unspecified), new Guid("9055afc0-351b-4dcd-80cf-11b1fb0a729b"), null, null, null, 2, 1, "currency", "PKR" },
                    { 4, new DateTime(2024, 9, 29, 19, 45, 33, 0, DateTimeKind.Unspecified), new Guid("9055afc0-351b-4dcd-80cf-11b1fb0a729b"), null, null, null, 2, 1, "contact_number", "03335662558" },
                    { 5, new DateTime(2024, 9, 29, 19, 45, 33, 0, DateTimeKind.Unspecified), new Guid("9055afc0-351b-4dcd-80cf-11b1fb0a729b"), null, null, null, 2, 1, "contact_email", "support@codingsips.com" },
                    { 6, new DateTime(2024, 9, 29, 19, 45, 33, 0, DateTimeKind.Unspecified), new Guid("9055afc0-351b-4dcd-80cf-11b1fb0a729b"), null, null, null, 2, 1, "max_upload_size_in_mbs", "5" },
                    { 7, new DateTime(2024, 9, 29, 19, 45, 33, 0, DateTimeKind.Unspecified), new Guid("9055afc0-351b-4dcd-80cf-11b1fb0a729b"), null, null, null, 2, 1, "allowed_upload_extensions", ".jpg,.jpeg,.png,.pdf,.docx,.xlsx,.txt" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "IsAdmin", "IsLocked", "LastLogin", "LockoutEnabled", "LockoutEnd", "MemberSince", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "address", "created", "created_by", "dob", "dp", "gender_id", "modified", "modified_by", "nationality_id", "remarks", "status" },
                values: new object[,]
                {
                    { "1da7364d-fc6b-4e3e-b290-56727fec2c65", 0, "276362ce-72e3-4fef-a19e-54ff316295b4", "alamadcs@gmail.com", true, "Alam", false, false, null, false, null, new DateTime(2024, 10, 1, 14, 56, 34, 546, DateTimeKind.Local).AddTicks(7440), "ALAMADCS@GMAIL.COM", "ALAMADCS@GMAIL.COM", "AQAAAAIAAYagAAAAEAgwzx5s+AhmTjSDN9xXrHZDKNl8YG8xao+5jUGw9tyTc+hDJX/R73bgdgTtRpEqng==", "+121111111111", true, "014b6990-e713-414c-86d4-8467adc71061", false, "alamadcs@gmail.com", "UAE", new DateTime(2024, 9, 29, 19, 45, 33, 0, DateTimeKind.Unspecified), 1, new DateOnly(1991, 9, 29), "user2.png", 1, null, null, 1, null, 1 },
                    { "9055afc0-351b-4dcd-80cf-11b1fb0a729b", 0, "6ab77101-b4bc-4dc9-9986-cc508e30d30d", "alamnaryab@gmail.com", true, "Fakhre Alam", true, false, null, false, null, new DateTime(2024, 10, 1, 14, 56, 34, 546, DateTimeKind.Local).AddTicks(7381), "ALAMNARYAB@GMAIL.COM", "ALAMNARYAB@GMAIL.COM", "AQAAAAIAAYagAAAAEAgwzx5s+AhmTjSDN9xXrHZDKNl8YG8xao+5jUGw9tyTc+hDJX/R73bgdgTtRpEqng==", "+111111111111", true, "04d6b296-74a4-4b6b-ac6d-3a693cd8ccb1", false, "alamnaryab@gmail.com", "UAE", new DateTime(2024, 9, 29, 19, 45, 33, 0, DateTimeKind.Unspecified), 1, new DateOnly(1992, 9, 29), "user1.png", 1, null, null, 1, null, 1 }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "e32c6f7a-3cf5-4ee0-9d1c-13e4af64d364", "1da7364d-fc6b-4e3e-b290-56727fec2c65" },
                    { "12329f10-ef4e-4922-bb05-3e7a3ffdd125", "9055afc0-351b-4dcd-80cf-11b1fb0a729b" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_gender_id",
                table: "AspNetUsers",
                column: "gender_id");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_nationality_id",
                table: "AspNetUsers",
                column: "nationality_id");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_LoginLogs_user_id",
                table: "LoginLogs",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "LoginLogs");

            migrationBuilder.DropTable(
                name: "Settings");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Genders");

            migrationBuilder.DropTable(
                name: "Nationalities");
        }
    }
}
