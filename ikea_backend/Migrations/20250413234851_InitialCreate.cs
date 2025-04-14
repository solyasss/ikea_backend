using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ikea_backend.Migrations
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
                table: "Sets",
                columns: new[] { "Id", "ImageUrl", "Name", "Slug" },
                values: new object[,]
                {
                    { 1, "/src/assets/img/furniture/furniture-1.png", "Набор №1", "furniture-set-1" },
                    { 2, "/src/assets/img/furniture/furniture-2.png", "Набор №2", "furniture-set-2" },
                    { 3, "/src/assets/img/furniture/furniture-3.png", "Набор №3", "furniture-set-3" }
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
                name: "ProductCharacteristics");

            migrationBuilder.DropTable(
                name: "ProductComments");

            migrationBuilder.DropTable(
                name: "ProductImages");

            migrationBuilder.DropTable(
                name: "SetItems");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Sets");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
