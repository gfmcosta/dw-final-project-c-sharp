using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DW_Final_Project.Data.Migrations
{
    /// <inheritdoc />
    public partial class addseeds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "Category",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "Type",
                columns: new[] { "id", "description" },
                values: new object[,]
                {
                    { 1, "admin" },
                    { 2, "client" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "Category",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);
        }
    }
}
