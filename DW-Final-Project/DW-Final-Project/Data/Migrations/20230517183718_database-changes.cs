using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DW_Final_Project.Data.Migrations
{
    /// <inheritdoc />
    public partial class databasechanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateTime",
                table: "Person",
                newName: "dataNasc");

            migrationBuilder.AlterColumn<int>(
                name: "quantity",
                table: "Product",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "dataNasc",
                table: "Person",
                newName: "DateTime");

            migrationBuilder.AlterColumn<string>(
                name: "quantity",
                table: "Product",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
