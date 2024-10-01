using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Commerce.Migrations
{
    /// <inheritdoc />
    public partial class items_featured_pic_made_optional : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "featured_pic",
                table: "Items",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1da7364d-fc6b-4e3e-b290-56727fec2c65",
                column: "MemberSince",
                value: new DateTime(2024, 10, 1, 17, 46, 25, 620, DateTimeKind.Local).AddTicks(6171));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9055afc0-351b-4dcd-80cf-11b1fb0a729b",
                column: "MemberSince",
                value: new DateTime(2024, 10, 1, 17, 46, 25, 620, DateTimeKind.Local).AddTicks(6125));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "featured_pic",
                table: "Items",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1da7364d-fc6b-4e3e-b290-56727fec2c65",
                column: "MemberSince",
                value: new DateTime(2024, 10, 1, 17, 2, 27, 781, DateTimeKind.Local).AddTicks(9372));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9055afc0-351b-4dcd-80cf-11b1fb0a729b",
                column: "MemberSince",
                value: new DateTime(2024, 10, 1, 17, 2, 27, 781, DateTimeKind.Local).AddTicks(9322));
        }
    }
}
