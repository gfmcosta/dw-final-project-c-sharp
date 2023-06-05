using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DW_Final_Project.Migrations
{
    /// <inheritdoc />
    public partial class Removedpersonfromusertable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Person_userFK",
                table: "Person");

            migrationBuilder.CreateIndex(
                name: "IX_Person_userFK",
                table: "Person",
                column: "userFK");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Person_userFK",
                table: "Person");

            migrationBuilder.CreateIndex(
                name: "IX_Person_userFK",
                table: "Person",
                column: "userFK",
                unique: true);
        }
    }
}
