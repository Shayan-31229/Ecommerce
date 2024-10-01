using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace E_Commerce.Migrations
{
    /// <inheritdoc />
    public partial class ItemAndRelatedModelsCreatedWithSeeds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
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
                    table.PrimaryKey("PK_Categories", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Colors",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    bgcolor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    textcolor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    sort = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    created_by = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    modified_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colors", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Sizes",
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
                    table.PrimaryKey("PK_Sizes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "SubCategories",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    category_id = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_SubCategories", x => x.id);
                    table.ForeignKey(
                        name: "FK_SubCategories_Categories_category_id",
                        column: x => x.category_id,
                        principalTable: "Categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EndCategories",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sub_category_id = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_EndCategories", x => x.id);
                    table.ForeignKey(
                        name: "FK_EndCategories_SubCategories_sub_category_id",
                        column: x => x.sub_category_id,
                        principalTable: "SubCategories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    end_category_id = table.Column<int>(type: "int", nullable: false),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    barcode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    old_price = table.Column<double>(type: "float", nullable: false),
                    price = table.Column<double>(type: "float", nullable: false),
                    qty = table.Column<int>(type: "int", nullable: false),
                    featured_pic = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    short_description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    features = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    terms_conditions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    hits = table.Column<int>(type: "int", nullable: false),
                    is_featured = table.Column<int>(type: "int", nullable: false),
                    sort = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    created_by = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    modified_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.id);
                    table.ForeignKey(
                        name: "FK_Items_EndCategories_end_category_id",
                        column: x => x.end_category_id,
                        principalTable: "EndCategories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemColors",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    item_id = table.Column<int>(type: "int", nullable: false),
                    color_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemColors", x => x.id);
                    table.ForeignKey(
                        name: "FK_ItemColors_Colors_color_id",
                        column: x => x.color_id,
                        principalTable: "Colors",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemColors_Items_item_id",
                        column: x => x.item_id,
                        principalTable: "Items",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemPhotos",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    item_id = table.Column<int>(type: "int", nullable: false),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    pic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    sort = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    created_by = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    modified_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemPhotos", x => x.id);
                    table.ForeignKey(
                        name: "FK_ItemPhotos_Items_item_id",
                        column: x => x.item_id,
                        principalTable: "Items",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemSizes",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    item_id = table.Column<int>(type: "int", nullable: false),
                    size_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemSizes", x => x.id);
                    table.ForeignKey(
                        name: "FK_ItemSizes_Items_item_id",
                        column: x => x.item_id,
                        principalTable: "Items",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemSizes_Sizes_size_id",
                        column: x => x.size_id,
                        principalTable: "Sizes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "id", "created", "created_by", "modified", "modified_by", "sort", "status", "title" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 9, 29, 19, 45, 33, 0, DateTimeKind.Unspecified), new Guid("9055afc0-351b-4dcd-80cf-11b1fb0a729b"), null, null, 1, 1, "Men" },
                    { 2, new DateTime(2024, 9, 29, 19, 45, 33, 0, DateTimeKind.Unspecified), new Guid("9055afc0-351b-4dcd-80cf-11b1fb0a729b"), null, null, 2, 1, "Women" },
                    { 3, new DateTime(2024, 9, 29, 19, 45, 33, 0, DateTimeKind.Unspecified), new Guid("9055afc0-351b-4dcd-80cf-11b1fb0a729b"), null, null, 3, 1, "Kids" }
                });

            migrationBuilder.InsertData(
                table: "Colors",
                columns: new[] { "id", "bgcolor", "created", "created_by", "modified", "modified_by", "sort", "status", "textcolor", "title" },
                values: new object[,]
                {
                    { 1, "#ff0000", new DateTime(2024, 9, 29, 19, 45, 33, 0, DateTimeKind.Unspecified), new Guid("9055afc0-351b-4dcd-80cf-11b1fb0a729b"), null, null, 1, 1, "#ffffff", "Red" },
                    { 2, "#000000", new DateTime(2024, 9, 29, 19, 45, 33, 0, DateTimeKind.Unspecified), new Guid("9055afc0-351b-4dcd-80cf-11b1fb0a729b"), null, null, 2, 1, "#ffffff", "Black" },
                    { 3, "#0000ff", new DateTime(2024, 9, 29, 19, 45, 33, 0, DateTimeKind.Unspecified), new Guid("9055afc0-351b-4dcd-80cf-11b1fb0a729b"), null, null, 3, 1, "#ffffff", "Blue" },
                    { 4, "#ffeb3b", new DateTime(2024, 9, 29, 19, 45, 33, 0, DateTimeKind.Unspecified), new Guid("9055afc0-351b-4dcd-80cf-11b1fb0a729b"), null, null, 4, 1, "#000000", "Yellow" }
                });

            migrationBuilder.InsertData(
                table: "Sizes",
                columns: new[] { "id", "created", "created_by", "modified", "modified_by", "sort", "status", "title" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 9, 29, 19, 45, 33, 0, DateTimeKind.Unspecified), new Guid("9055afc0-351b-4dcd-80cf-11b1fb0a729b"), null, null, 1, 1, "S" },
                    { 2, new DateTime(2024, 9, 29, 19, 45, 33, 0, DateTimeKind.Unspecified), new Guid("9055afc0-351b-4dcd-80cf-11b1fb0a729b"), null, null, 2, 1, "M" },
                    { 3, new DateTime(2024, 9, 29, 19, 45, 33, 0, DateTimeKind.Unspecified), new Guid("9055afc0-351b-4dcd-80cf-11b1fb0a729b"), null, null, 3, 1, "L" },
                    { 4, new DateTime(2024, 9, 29, 19, 45, 33, 0, DateTimeKind.Unspecified), new Guid("9055afc0-351b-4dcd-80cf-11b1fb0a729b"), null, null, 4, 1, "XL" }
                });

            migrationBuilder.InsertData(
                table: "SubCategories",
                columns: new[] { "id", "category_id", "created", "created_by", "modified", "modified_by", "sort", "status", "title" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 9, 29, 19, 45, 33, 0, DateTimeKind.Unspecified), new Guid("9055afc0-351b-4dcd-80cf-11b1fb0a729b"), null, null, 1, 1, "Men Accessories" },
                    { 2, 1, new DateTime(2024, 9, 29, 19, 45, 33, 0, DateTimeKind.Unspecified), new Guid("9055afc0-351b-4dcd-80cf-11b1fb0a729b"), null, null, 2, 1, "Shoes" },
                    { 3, 2, new DateTime(2024, 9, 29, 19, 45, 33, 0, DateTimeKind.Unspecified), new Guid("9055afc0-351b-4dcd-80cf-11b1fb0a729b"), null, null, 3, 1, "Women Accessories" },
                    { 4, 2, new DateTime(2024, 9, 29, 19, 45, 33, 0, DateTimeKind.Unspecified), new Guid("9055afc0-351b-4dcd-80cf-11b1fb0a729b"), null, null, 2, 1, "Women Shoes" },
                    { 5, 3, new DateTime(2024, 9, 29, 19, 45, 33, 0, DateTimeKind.Unspecified), new Guid("9055afc0-351b-4dcd-80cf-11b1fb0a729b"), null, null, 3, 1, "Kids Accessories" },
                    { 6, 3, new DateTime(2024, 9, 29, 19, 45, 33, 0, DateTimeKind.Unspecified), new Guid("9055afc0-351b-4dcd-80cf-11b1fb0a729b"), null, null, 3, 1, "Kids Shoes" }
                });

            migrationBuilder.InsertData(
                table: "EndCategories",
                columns: new[] { "id", "created", "created_by", "modified", "modified_by", "sort", "status", "sub_category_id", "title" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 9, 29, 19, 45, 33, 0, DateTimeKind.Unspecified), new Guid("9055afc0-351b-4dcd-80cf-11b1fb0a729b"), null, null, 1, 1, 1, "Watches" },
                    { 2, new DateTime(2024, 9, 29, 19, 45, 33, 0, DateTimeKind.Unspecified), new Guid("9055afc0-351b-4dcd-80cf-11b1fb0a729b"), null, null, 2, 1, 1, "Suits" },
                    { 3, new DateTime(2024, 9, 29, 19, 45, 33, 0, DateTimeKind.Unspecified), new Guid("9055afc0-351b-4dcd-80cf-11b1fb0a729b"), null, null, 3, 1, 2, "Slippers" },
                    { 4, new DateTime(2024, 9, 29, 19, 45, 33, 0, DateTimeKind.Unspecified), new Guid("9055afc0-351b-4dcd-80cf-11b1fb0a729b"), null, null, 2, 1, 2, "Jogers" },
                    { 5, new DateTime(2024, 9, 29, 19, 45, 33, 0, DateTimeKind.Unspecified), new Guid("9055afc0-351b-4dcd-80cf-11b1fb0a729b"), null, null, 3, 1, 3, "Suits" },
                    { 6, new DateTime(2024, 9, 29, 19, 45, 33, 0, DateTimeKind.Unspecified), new Guid("9055afc0-351b-4dcd-80cf-11b1fb0a729b"), null, null, 3, 1, 3, "Dopatas" },
                    { 7, new DateTime(2024, 9, 29, 19, 45, 33, 0, DateTimeKind.Unspecified), new Guid("9055afc0-351b-4dcd-80cf-11b1fb0a729b"), null, null, 2, 1, 4, "Sandals" },
                    { 8, new DateTime(2024, 9, 29, 19, 45, 33, 0, DateTimeKind.Unspecified), new Guid("9055afc0-351b-4dcd-80cf-11b1fb0a729b"), null, null, 3, 1, 5, "Dipers" },
                    { 9, new DateTime(2024, 9, 29, 19, 45, 33, 0, DateTimeKind.Unspecified), new Guid("9055afc0-351b-4dcd-80cf-11b1fb0a729b"), null, null, 2, 1, 6, "Caps" },
                    { 10, new DateTime(2024, 9, 29, 19, 45, 33, 0, DateTimeKind.Unspecified), new Guid("9055afc0-351b-4dcd-80cf-11b1fb0a729b"), null, null, 3, 1, 6, "Suits" },
                    { 11, new DateTime(2024, 9, 29, 19, 45, 33, 0, DateTimeKind.Unspecified), new Guid("9055afc0-351b-4dcd-80cf-11b1fb0a729b"), null, null, 3, 1, 6, "Towels" }
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "id", "barcode", "created", "created_by", "description", "end_category_id", "featured_pic", "features", "hits", "is_featured", "modified", "modified_by", "old_price", "price", "qty", "remarks", "short_description", "sort", "status", "terms_conditions", "title" },
                values: new object[,]
                {
                    { 1, "123456789", new DateTime(2024, 9, 29, 19, 45, 33, 0, DateTimeKind.Unspecified), new Guid("9055afc0-351b-4dcd-80cf-11b1fb0a729b"), null, 1, "1.jpg", null, 1, 1, null, null, 177.0, 99.0, 123, null, null, 1, 1, null, "Rolex T20" },
                    { 2, "123456789", new DateTime(2024, 9, 29, 19, 45, 33, 0, DateTimeKind.Unspecified), new Guid("9055afc0-351b-4dcd-80cf-11b1fb0a729b"), null, 1, "2.jpg", null, 1, 1, null, null, 177.0, 99.0, 123, null, null, 2, 1, null, "Apple Smart watch i16 pro max" },
                    { 3, "123456789", new DateTime(2024, 9, 29, 19, 45, 33, 0, DateTimeKind.Unspecified), new Guid("9055afc0-351b-4dcd-80cf-11b1fb0a729b"), null, 2, "3.jpg", null, 1, 1, null, null, 177.0, 99.0, 123, null, null, 3, 1, null, "Bannu warai cloths w99" },
                    { 4, "123456789", new DateTime(2024, 9, 29, 19, 45, 33, 0, DateTimeKind.Unspecified), new Guid("9055afc0-351b-4dcd-80cf-11b1fb0a729b"), null, 3, "1.jpg", null, 1, 1, null, null, 177.0, 99.0, 123, null, null, 1, 1, null, "Lahori Chappal" },
                    { 5, "123456789", new DateTime(2024, 9, 29, 19, 45, 33, 0, DateTimeKind.Unspecified), new Guid("9055afc0-351b-4dcd-80cf-11b1fb0a729b"), null, 4, "2.jpg", null, 1, 1, null, null, 177.0, 99.0, 123, null, null, 2, 1, null, "Batta Sports T35" },
                    { 6, "123456789", new DateTime(2024, 9, 29, 19, 45, 33, 0, DateTimeKind.Unspecified), new Guid("9055afc0-351b-4dcd-80cf-11b1fb0a729b"), null, 4, "3.jpg", null, 1, 1, null, null, 177.0, 99.0, 123, null, null, 3, 1, null, "Service plain shoes" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_EndCategories_sub_category_id",
                table: "EndCategories",
                column: "sub_category_id");

            migrationBuilder.CreateIndex(
                name: "IX_ItemColors_color_id",
                table: "ItemColors",
                column: "color_id");

            migrationBuilder.CreateIndex(
                name: "IX_ItemColors_item_id",
                table: "ItemColors",
                column: "item_id");

            migrationBuilder.CreateIndex(
                name: "IX_ItemPhotos_item_id",
                table: "ItemPhotos",
                column: "item_id");

            migrationBuilder.CreateIndex(
                name: "IX_Items_end_category_id",
                table: "Items",
                column: "end_category_id");

            migrationBuilder.CreateIndex(
                name: "IX_ItemSizes_item_id",
                table: "ItemSizes",
                column: "item_id");

            migrationBuilder.CreateIndex(
                name: "IX_ItemSizes_size_id",
                table: "ItemSizes",
                column: "size_id");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategories_category_id",
                table: "SubCategories",
                column: "category_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemColors");

            migrationBuilder.DropTable(
                name: "ItemPhotos");

            migrationBuilder.DropTable(
                name: "ItemSizes");

            migrationBuilder.DropTable(
                name: "Colors");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Sizes");

            migrationBuilder.DropTable(
                name: "EndCategories");

            migrationBuilder.DropTable(
                name: "SubCategories");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1da7364d-fc6b-4e3e-b290-56727fec2c65",
                column: "MemberSince",
                value: new DateTime(2024, 10, 1, 14, 56, 34, 546, DateTimeKind.Local).AddTicks(7440));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9055afc0-351b-4dcd-80cf-11b1fb0a729b",
                column: "MemberSince",
                value: new DateTime(2024, 10, 1, 14, 56, 34, 546, DateTimeKind.Local).AddTicks(7381));
        }
    }
}
