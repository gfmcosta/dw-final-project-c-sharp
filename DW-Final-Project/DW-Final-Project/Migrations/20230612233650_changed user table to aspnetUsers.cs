using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DW_Final_Project.Migrations
{
    /// <inheritdoc />
    public partial class changedusertabletoaspnetUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Person_User_userFK",
                table: "Person");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Type");

            migrationBuilder.DropIndex(
                name: "IX_Person_userFK",
                table: "Person");

            migrationBuilder.DeleteData(
                table: "Person",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Person",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Person",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "userFK",
                table: "Person");

            migrationBuilder.AddColumn<string>(
                name: "userId",
                table: "Person",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "userId",
                table: "Person");

            migrationBuilder.AddColumn<int>(
                name: "userFK",
                table: "Person",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Type",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Type", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    typeFK = table.Column<int>(type: "int", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    token = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.id);
                    table.ForeignKey(
                        name: "FK_User_Type_typeFK",
                        column: x => x.typeFK,
                        principalTable: "Type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Type",
                columns: new[] { "id", "description" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "Cliente" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "id", "email", "password", "token", "typeFK" },
                values: new object[,]
                {
                    { 1, "goncalo.costa@gmail.com", "7efe01a7a37b674f902aaaa6385f991e72018563f9c4280691bbc593988703d4", null, 1 },
                    { 2, "joao.goncalves@gmail.com", "63831aef4d4c7fc4f58d430ae5cf5fb6d9b04b475dcbd8df6a5b57db6ae841ee", null, 1 },
                    { 3, "jose.silva@gmail.com", "dad91e6a5a72560ba402a95f2a4cc43f57f2d300a26d417585ae8491a47540cc", null, 2 }
                });

            migrationBuilder.InsertData(
                table: "Person",
                columns: new[] { "id", "address", "dataNasc", "gender", "imagePath", "name", "phoneNumber", "postalCode", "userFK" },
                values: new object[,]
                {
                    { 1, "Rua das Flores 31 2D", new DateTime(2003, 1, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "M", "default-m.png", "Gonçalo Costa", "925863873", "2605-141 BELAS", 1 },
                    { 2, "Rua das Papoilas 21 1Esq", new DateTime(2003, 5, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "M", "default-m.png", "João Gonçalves", "924665908", "2300-674 TOMAR", 2 },
                    { 3, "Quinta do Contador 4", new DateTime(1976, 7, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "M", "default-m.png", "José Silva", "913765880", "2300-313 TOMAR", 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Person_userFK",
                table: "Person",
                column: "userFK");

            migrationBuilder.CreateIndex(
                name: "IX_User_typeFK",
                table: "User",
                column: "typeFK");

            migrationBuilder.AddForeignKey(
                name: "FK_Person_User_userFK",
                table: "Person",
                column: "userFK",
                principalTable: "User",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
