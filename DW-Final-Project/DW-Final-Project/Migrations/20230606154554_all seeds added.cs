using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DW_Final_Project.Migrations
{
    /// <inheritdoc />
    public partial class allseedsadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Person",
                keyColumn: "id",
                keyValue: 1,
                column: "imagePath",
                value: "default-m.png");

            migrationBuilder.UpdateData(
                table: "Person",
                keyColumn: "id",
                keyValue: 2,
                column: "imagePath",
                value: "default-m.png");

            migrationBuilder.UpdateData(
                table: "Person",
                keyColumn: "id",
                keyValue: 3,
                column: "imagePath",
                value: "default-m.png");

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 1,
                column: "imagePath",
                value: "default-c.png");

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 2,
                column: "imagePath",
                value: "default-c.png");

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 3,
                column: "imagePath",
                value: "default-c.png");

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 4,
                column: "imagePath",
                value: "default-c.png");

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 5,
                column: "imagePath",
                value: "default-c.png");

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 6,
                column: "imagePath",
                value: "default-c.png");

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 7,
                column: "imagePath",
                value: "default-c.png");

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 8,
                column: "imagePath",
                value: "default-c.png");

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 9,
                column: "imagePath",
                value: "default-c.png");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Person",
                keyColumn: "id",
                keyValue: 1,
                column: "imagePath",
                value: "default-m");

            migrationBuilder.UpdateData(
                table: "Person",
                keyColumn: "id",
                keyValue: 2,
                column: "imagePath",
                value: "default-m");

            migrationBuilder.UpdateData(
                table: "Person",
                keyColumn: "id",
                keyValue: 3,
                column: "imagePath",
                value: "default-m");

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 1,
                column: "imagePath",
                value: null);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 2,
                column: "imagePath",
                value: null);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 3,
                column: "imagePath",
                value: null);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 4,
                column: "imagePath",
                value: null);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 5,
                column: "imagePath",
                value: null);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 6,
                column: "imagePath",
                value: null);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 7,
                column: "imagePath",
                value: null);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 8,
                column: "imagePath",
                value: null);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 9,
                column: "imagePath",
                value: null);
        }
    }
}
