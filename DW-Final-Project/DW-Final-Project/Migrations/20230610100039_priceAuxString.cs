using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DW_Final_Project.Migrations
{
    /// <inheritdoc />
    public partial class priceAuxString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "id",
                keyValue: 2,
                column: "password",
                value: "63831aef4d4c7fc4f58d430ae5cf5fb6d9b04b475dcbd8df6a5b57db6ae841ee");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "id",
                keyValue: 2,
                column: "password",
                value: "622cc9ae3a18440b2288dba66daa9d655af0994ac3f6aecea4b4cf607277bea8");
        }
    }
}
