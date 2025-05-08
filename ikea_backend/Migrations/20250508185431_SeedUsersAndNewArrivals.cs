using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ikea_backend.Migrations
{
    /// <inheritdoc />
    public partial class SeedUsersAndNewArrivals : Migration
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NewArrivals");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
