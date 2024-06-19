using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class IncluindoChaveEstrangeiraDeClienteEmProcesso : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0bf62e31-15c8-4354-8d89-866b734ae7e2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "72d0b260-2b39-4ffd-8b46-556b5078da63");

            migrationBuilder.AddColumn<int>(
                name: "ClienteId",
                table: "Processo",
                type: "int",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "709cc417-4e76-4e15-83e1-a616197dfdad", "1f5951db-5c06-4de0-89c3-42a66333b1d5", "Usuário", "USUÁRIO" },
                    { "d60bdd70-bcd9-4650-b527-1fe2a9203f89", "ca8a5108-9709-4bf1-a47a-935248604bdd", "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Processo_ClienteId",
                table: "Processo",
                column: "ClienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Processo_Cliente_ClienteId",
                table: "Processo",
                column: "ClienteId",
                principalTable: "Cliente",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Processo_Cliente_ClienteId",
                table: "Processo");

            migrationBuilder.DropIndex(
                name: "IX_Processo_ClienteId",
                table: "Processo");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "709cc417-4e76-4e15-83e1-a616197dfdad");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d60bdd70-bcd9-4650-b527-1fe2a9203f89");

            migrationBuilder.DropColumn(
                name: "ClienteId",
                table: "Processo");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0bf62e31-15c8-4354-8d89-866b734ae7e2", "9771cc9a-938a-4dfb-8a3e-a5a2f2a751f3", "Admin", "ADMIN" },
                    { "72d0b260-2b39-4ffd-8b46-556b5078da63", "06fd0dcc-4d9e-4164-8f19-604b12abf836", "Usuário", "USUÁRIO" }
                });
        }
    }
}
