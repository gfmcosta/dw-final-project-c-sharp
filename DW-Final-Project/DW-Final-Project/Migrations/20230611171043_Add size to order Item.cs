using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DW_Final_Project.Migrations
{
    /// <inheritdoc />
    public partial class AddsizetoorderItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "size",
                table: "OrderItem",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "size",
                table: "OrderItem");
        }
    }
}
