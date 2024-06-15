using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class addmigraton : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0fffabb3-0c23-44e3-a694-a33ff4d70612");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b0468126-9a47-403a-b312-dbf2545ee421");

            migrationBuilder.AddColumn<string>(
                name: "PJECAcao",
                table: "ProcessosAtualizacao",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0bf62e31-15c8-4354-8d89-866b734ae7e2", "9771cc9a-938a-4dfb-8a3e-a5a2f2a751f3", "Admin", "ADMIN" },
                    { "72d0b260-2b39-4ffd-8b46-556b5078da63", "06fd0dcc-4d9e-4164-8f19-604b12abf836", "Usuário", "USUÁRIO" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0bf62e31-15c8-4354-8d89-866b734ae7e2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "72d0b260-2b39-4ffd-8b46-556b5078da63");

            migrationBuilder.DropColumn(
                name: "PJECAcao",
                table: "ProcessosAtualizacao");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0fffabb3-0c23-44e3-a694-a33ff4d70612", "bb06161b-b9dc-421a-a7fc-34670608b31d", "Usuário", "USUÁRIO" },
                    { "b0468126-9a47-403a-b312-dbf2545ee421", "ba98b687-d614-4dec-af5c-6bbc6b132707", "Admin", "ADMIN" }
                });
        }
    }
}
