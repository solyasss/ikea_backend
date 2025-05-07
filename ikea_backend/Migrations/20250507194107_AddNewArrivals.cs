using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ikea_backend.Migrations
{
    /// <inheritdoc />
    public partial class AddNewArrivals : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                table: "NewArrivals",
                columns: new[] { "Id", "ImageUrl", "Text" },
                values: new object[,]
                {
                    { 1, "/img/new_options_products/set-1.png", "Sofa" },
                    { 2, "/img/new_options_products/set-2.png", "Decoration" },
                    { 3, "/img/new_options_products/set-3.png", "Pillow" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NewArrivals");
        }
    }
}
