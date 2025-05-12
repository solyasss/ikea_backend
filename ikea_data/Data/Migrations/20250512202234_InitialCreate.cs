using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ikea_data.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Categories_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "NewArrivals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewArrivals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MainImage = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductCharacteristics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCharacteristics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductCharacteristics_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductComments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommentText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductComments_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductImages_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SetItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SetId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SetItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SetItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SetItems_Sets_SetId",
                        column: x => x.SetId,
                        principalTable: "Sets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "ParentId", "Slug", "Title" },
                values: new object[,]
                {
                    { 1, null, "living-room", "Гостиная" },
                    { 2, null, "bedroom", "Спальня" },
                    { 3, null, "kitchen-and-dining", "Кухня и столовая" },
                    { 4, null, "bathroom", "Ванная" },
                    { 5, null, "office", "Офис" }
                });

            migrationBuilder.InsertData(
                table: "NewArrivals",
                columns: new[] { "Id", "ImageUrl", "Text" },
                values: new object[,]
                {
                    { 1, "/img/new_options_products/set-1.png", "Sofa" },
                    { 2, "/img/new_options_products/set-2.png", "Decoration" },
                    { 3, "/img/new_options_products/set-3.png", "Pillow" }
                });

            migrationBuilder.InsertData(
                table: "Sets",
                columns: new[] { "Id", "ImageUrl", "Name", "Slug" },
                values: new object[,]
                {
                    { 1, "/src/assets/img/furniture/furniture-1.png", "Набор №1", "furniture-set-1" },
                    { 2, "/src/assets/img/furniture/furniture-2.png", "Набор №2", "furniture-set-2" },
                    { 3, "/src/assets/img/furniture/furniture-3.png", "Набор №3", "furniture-set-3" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "BirthDate", "Country", "Email", "FirstName", "IsAdmin", "LastName", "PasswordHash", "PasswordSalt", "Phone" },
                values: new object[,]
                {
                    { 1, "1 Admin Road", new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "UK", "admin@ikea.com", "Admin", true, "User", new byte[] { 45, 254, 197, 241, 52, 176, 158, 177, 161, 196, 3, 238, 199, 21, 216, 118, 144, 194, 245, 84, 164, 75, 58, 194, 36, 9, 25, 108, 6, 99, 228, 33, 139, 155, 202, 113, 155, 186, 250, 65, 155, 67, 102, 50, 207, 39, 84, 219, 78, 7, 220, 147, 79, 143, 120, 18, 214, 97, 63, 41, 244, 122, 55, 143 }, new byte[] { 36, 111, 179, 12, 157, 148, 208, 59, 111, 135, 113, 38, 205, 21, 158, 76, 135, 182, 206, 8, 211, 153, 207, 127, 55, 41, 100, 164, 6, 72, 161, 20, 14, 31, 23, 71, 126, 202, 141, 141 }, "+441234567890" },
                    { 2, "123 Maple Street", new DateTime(1992, 3, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "USA", "bob@ikea.com", "Bob", false, "Brown", new byte[] { 115, 81, 128, 43, 103, 236, 230, 23, 161, 244, 85, 105, 36, 200, 204, 144, 92, 77, 228, 200, 135, 27, 134, 214, 5, 144, 209, 108, 232, 59, 181, 129, 73, 147, 136, 169, 126, 222, 47, 3, 195, 233, 92, 189, 24, 245, 144, 121, 138, 152, 254, 169, 73, 204, 117, 134, 52, 121, 67, 189, 241, 89, 222, 246 }, new byte[] { 209, 205, 100, 64, 91, 176, 213, 110, 163, 41, 162, 122, 9, 7, 202, 125, 87, 245, 34, 127, 65, 70, 132, 44, 13, 4, 194, 172, 201, 5, 238, 37, 43, 246, 219, 216, 208, 250, 217, 20 }, "+12125550000" },
                    { 3, "45 Maple Avenue", new DateTime(1991, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Canada", "charlie@ikea.com", "Charlie", false, "Clark", new byte[] { 232, 119, 88, 98, 52, 188, 108, 223, 77, 155, 170, 213, 27, 226, 135, 166, 156, 3, 206, 220, 196, 61, 85, 146, 31, 54, 12, 100, 60, 93, 181, 116, 47, 134, 70, 227, 217, 146, 218, 100, 194, 159, 70, 179, 64, 149, 81, 14, 132, 189, 100, 27, 37, 4, 235, 227, 40, 140, 238, 33, 196, 33, 22, 172 }, new byte[] { 228, 76, 139, 139, 34, 171, 251, 36, 145, 250, 122, 156, 119, 156, 204, 25, 195, 104, 59, 176, 199, 172, 233, 152, 102, 12, 131, 52, 165, 129, 106, 102, 78, 42, 207, 148, 172, 254, 207, 209 }, "+14165551234" },
                    { 4, "789 Berlin Str", new DateTime(1993, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Germany", "diana@ikea.com", "Diana", false, "Davis", new byte[] { 115, 216, 116, 115, 192, 185, 102, 113, 14, 197, 146, 71, 58, 122, 195, 170, 60, 247, 176, 14, 159, 11, 105, 216, 211, 133, 2, 33, 205, 161, 118, 186, 246, 156, 105, 195, 50, 228, 213, 188, 210, 39, 51, 231, 99, 68, 147, 226, 109, 240, 75, 164, 146, 116, 83, 57, 221, 122, 169, 1, 117, 51, 190, 207 }, new byte[] { 80, 200, 136, 26, 241, 246, 187, 44, 193, 147, 71, 100, 241, 75, 7, 42, 140, 244, 0, 146, 23, 187, 47, 119, 5, 52, 201, 141, 145, 88, 167, 47, 148, 46, 128, 122, 22, 209, 96, 63 }, "+4915112345678" },
                    { 5, "22 Rue de Lyon", new DateTime(1995, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "France", "evan@ikea.com", "Evan", false, "Evans", new byte[] { 247, 120, 15, 64, 158, 111, 116, 152, 116, 50, 133, 8, 33, 56, 206, 180, 226, 5, 186, 61, 191, 86, 213, 27, 242, 198, 216, 100, 30, 90, 21, 91, 193, 204, 222, 176, 89, 161, 201, 146, 54, 213, 207, 185, 148, 222, 215, 76, 141, 186, 24, 161, 25, 167, 95, 232, 194, 224, 169, 221, 209, 233, 58, 48 }, new byte[] { 125, 121, 167, 221, 251, 100, 243, 19, 193, 35, 240, 85, 178, 115, 16, 166, 3, 229, 71, 2, 18, 151, 72, 3, 133, 128, 109, 94, 102, 212, 160, 141, 41, 11, 49, 19, 15, 86, 107, 176 }, "+33142000000" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "ParentId", "Slug", "Title" },
                values: new object[,]
                {
                    { 6, 1, "sofas", "Диваны" },
                    { 7, 1, "armchairs", "Кресла" },
                    { 8, 1, "coffee-tables", "Журнальные столики" },
                    { 9, 1, "wardrobes-and-shelves", "Шкафы и полки" },
                    { 10, 1, "tv-stands", "Стеллажи для ТВ" },
                    { 11, 1, "carpets", "Ковры" },
                    { 12, 1, "lighting", "Освещение" },
                    { 13, 1, "decor", "Декор" },
                    { 14, 1, "storage", "Системы хранения" },
                    { 15, 2, "beds", "Кровати" },
                    { 16, 2, "mattresses", "Матрасы" },
                    { 17, 2, "closets", "Шкафы" },
                    { 18, 3, "dining-tables", "Обеденные столы" },
                    { 19, 3, "chairs", "Стулья" },
                    { 20, 3, "bar-stools", "Барные стулья" },
                    { 21, 3, "dishes", "Посуда" },
                    { 22, 3, "table-setting", "Сервировка стола" },
                    { 23, 3, "cutlery", "Столовые приборы" },
                    { 24, 4, "shelves-and-cabinets", "Полки и шкафчики" },
                    { 25, 4, "towels", "Полотенца" },
                    { 26, 4, "shower-curtains", "Шторки для душа" },
                    { 27, 5, "desks", "Письменные столы" },
                    { 28, 5, "office-chairs", "Офисные кресла" },
                    { 29, 5, "bookshelves", "Книжные шкафы" },
                    { 30, 5, "desk-lamps", "Настольные лампы" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "MainImage", "Name", "Price", "Slug" },
                values: new object[,]
                {
                    { 1, 19, "/src/assets/img/products/product-1.png", "Chair", 59.99m, "chair-1" },
                    { 2, 12, "/src/assets/img/products/product-2.png", "Lamp", 19.99m, "lamp-2" },
                    { 3, 12, "/src/assets/img/products/product-3.png", "Lamp", 19.99m, "lamp-3" }
                });

            migrationBuilder.InsertData(
                table: "ProductCharacteristics",
                columns: new[] { "Id", "Name", "ProductId", "Value" },
                values: new object[,]
                {
                    { 1, "Material", 1, "Wood" },
                    { 2, "Color", 1, "Brown" },
                    { 3, "Color", 2, "White" }
                });

            migrationBuilder.InsertData(
                table: "ProductComments",
                columns: new[] { "Id", "CommentText", "ProductId", "Rating", "UserName" },
                values: new object[,]
                {
                    { 1, "Nice chair", 1, 5, "User1" },
                    { 2, "Bright lamp", 2, 4, "User2" }
                });

            migrationBuilder.InsertData(
                table: "ProductImages",
                columns: new[] { "Id", "ImageUrl", "ProductId", "SortOrder" },
                values: new object[,]
                {
                    { 1, "/src/assets/img/products/product-1.png", 1, 0 },
                    { 2, "/src/assets/img/products/product-2.png", 2, 0 },
                    { 3, "/src/assets/img/products/product-3.png", 3, 0 }
                });

            migrationBuilder.InsertData(
                table: "SetItems",
                columns: new[] { "Id", "ProductId", "Quantity", "SetId" },
                values: new object[,]
                {
                    { 1, 1, 2, 1 },
                    { 2, 2, 1, 1 },
                    { 3, 2, 2, 2 },
                    { 4, 3, 1, 2 },
                    { 5, 1, 1, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParentId",
                table: "Categories",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCharacteristics_ProductId",
                table: "ProductCharacteristics",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductComments_ProductId",
                table: "ProductComments",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductId",
                table: "ProductImages",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SetItems_ProductId",
                table: "SetItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_SetItems_SetId",
                table: "SetItems",
                column: "SetId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NewArrivals");

            migrationBuilder.DropTable(
                name: "ProductCharacteristics");

            migrationBuilder.DropTable(
                name: "ProductComments");

            migrationBuilder.DropTable(
                name: "ProductImages");

            migrationBuilder.DropTable(
                name: "SetItems");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Sets");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
