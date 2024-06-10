using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class TestandoRelAdvProcesso : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Processo_Advogado_AdvogadoId",
                table: "Processo");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "78713cfd-843c-4c5f-94e8-2aa9c817d721");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b01aa80a-7466-49a2-be8d-273da1cdb2d9");

            migrationBuilder.AlterColumn<int>(
                name: "AdvogadoId",
                table: "Processo",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0fffabb3-0c23-44e3-a694-a33ff4d70612", "bb06161b-b9dc-421a-a7fc-34670608b31d", "Usuário", "USUÁRIO" },
                    { "b0468126-9a47-403a-b312-dbf2545ee421", "ba98b687-d614-4dec-af5c-6bbc6b132707", "Admin", "ADMIN" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Processo_Advogado_AdvogadoId",
                table: "Processo",
                column: "AdvogadoId",
                principalTable: "Advogado",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Processo_Advogado_AdvogadoId",
                table: "Processo");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0fffabb3-0c23-44e3-a694-a33ff4d70612");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b0468126-9a47-403a-b312-dbf2545ee421");

            migrationBuilder.AlterColumn<int>(
                name: "AdvogadoId",
                table: "Processo",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "78713cfd-843c-4c5f-94e8-2aa9c817d721", "62648fd0-2daf-4391-ab12-09b8b1d95637", "Usuário", "USUÁRIO" },
                    { "b01aa80a-7466-49a2-be8d-273da1cdb2d9", "6c30c2b2-8a89-4cac-bf78-588d4c5ebdb4", "Admin", "ADMIN" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Processo_Advogado_AdvogadoId",
                table: "Processo",
                column: "AdvogadoId",
                principalTable: "Advogado",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
