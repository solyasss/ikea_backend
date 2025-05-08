using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ikea_backend.Migrations
{
    /// <inheritdoc />
    public partial class RestoreCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "ParentId", "Slug", "Title" },
                values: new object[,]
                {
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
                    { 29, 5, "bookshelves", "Книжные шкафы" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 29);
        }
    }
}
