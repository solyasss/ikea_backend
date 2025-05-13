using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ikea_data.Data.Migrations
{
    /// <inheritdoc />
    public partial class SetDecimalPrecision_SeedProductDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Weight",
                table: "Products",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Rating",
                table: "Products",
                type: "decimal(3,2)",
                precision: 3,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Products",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Color", "CountryOfOrigin", "Dimensions", "Materials", "Name", "PackageContents", "Rating", "Type", "Warranty", "Weight" },
                values: new object[] { "Brown", "Poland", "60x60x90", "Wood, Textile", "Comfort Chair", "1x Chair, Instructions", 4.5m, "Armchair", "2 years", 7.5m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Color", "CountryOfOrigin", "Dimensions", "Materials", "Name", "PackageContents", "Rating", "Type", "Warranty", "Weight" },
                values: new object[] { "White", "Germany", "15x15x45", "Plastic, Metal", "Minimalist Lamp", "1x Lamp, Bulb included", 4.0m, "Desk Lamp", "1 year", 1.2m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Color", "CountryOfOrigin", "Dimensions", "Materials", "Name", "PackageContents", "Rating", "Type", "Warranty", "Weight" },
                values: new object[] { "Black", "Italy", "18x18x50", "Metal, Glass", "Vintage Lamp", "1x Lamp", 3.8m, "Floor Lamp", "1 year", 1.5m });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Weight",
                table: "Products",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldPrecision: 8,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Rating",
                table: "Products",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,2)",
                oldPrecision: 3,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Products",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Color", "CountryOfOrigin", "Dimensions", "Materials", "Name", "PackageContents", "Rating", "Type", "Warranty", "Weight" },
                values: new object[] { null, null, null, null, "Chair", null, null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Color", "CountryOfOrigin", "Dimensions", "Materials", "Name", "PackageContents", "Rating", "Type", "Warranty", "Weight" },
                values: new object[] { null, null, null, null, "Lamp", null, null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Color", "CountryOfOrigin", "Dimensions", "Materials", "Name", "PackageContents", "Rating", "Type", "Warranty", "Weight" },
                values: new object[] { null, null, null, null, "Lamp", null, null, null, null, null });
        }
    }
}
