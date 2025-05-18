using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ikea_data.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedUserCardsOnly : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "UserCards",
                columns: new[] { "Id", "CardNumber", "CardType", "CvvHash", "UserId", "ValidDay", "ValidYear" },
                values: new object[,]
                {
                    { 1, "1111222233334444", "visa", new byte[] { 166, 101, 164, 89, 32, 66, 47, 157, 65, 126, 72, 103, 239, 220, 79, 184, 160, 74, 31, 63, 255, 31, 160, 126, 153, 142, 134, 247, 247, 162, 122, 227 }, 2, 1, 2026 },
                    { 2, "5555666677778888", "mastercard", new byte[] { 179, 168, 224, 225, 249, 171, 27, 254, 58, 54, 242, 49, 246, 118, 247, 139, 179, 10, 81, 157, 43, 33, 230, 197, 48, 192, 238, 232, 235, 180, 165, 208 }, 3, 15, 2025 },
                    { 3, "9999000011112222", "visa", new byte[] { 53, 169, 227, 129, 177, 162, 117, 103, 84, 155, 95, 138, 111, 120, 60, 22, 126, 191, 128, 159, 28, 77, 106, 158, 54, 114, 64, 72, 77, 140, 226, 129 }, 4, 30, 2027 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserCards",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "UserCards",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "UserCards",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
