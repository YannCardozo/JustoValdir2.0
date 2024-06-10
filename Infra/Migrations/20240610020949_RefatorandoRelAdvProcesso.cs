using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class RefatorandoRelAdvProcesso : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "05e600b7-7f1e-43f3-9d76-088c86003617");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "57d70f43-4cb5-4657-9ee9-14e5bc57b081");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "78713cfd-843c-4c5f-94e8-2aa9c817d721", "62648fd0-2daf-4391-ab12-09b8b1d95637", "Usuário", "USUÁRIO" },
                    { "b01aa80a-7466-49a2-be8d-273da1cdb2d9", "6c30c2b2-8a89-4cac-bf78-588d4c5ebdb4", "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "78713cfd-843c-4c5f-94e8-2aa9c817d721");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b01aa80a-7466-49a2-be8d-273da1cdb2d9");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "05e600b7-7f1e-43f3-9d76-088c86003617", "fc588386-d625-479a-8950-b71b96e07aaa", "Admin", "ADMIN" },
                    { "57d70f43-4cb5-4657-9ee9-14e5bc57b081", "c6d81463-54c7-4738-bf75-6aa230407ee6", "Usuário", "USUÁRIO" }
                });
        }
    }
}
