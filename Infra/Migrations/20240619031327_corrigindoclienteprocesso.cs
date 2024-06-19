using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class corrigindoclienteprocesso : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cliente_Processo_ProcessoId",
                table: "Cliente");

            migrationBuilder.DropIndex(
                name: "IX_Cliente_ProcessoId",
                table: "Cliente");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a06ed2d9-ad3e-4c8c-86af-6a0aba6a128b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b508f537-ebfb-4621-ad2a-c2699e680a47");

            migrationBuilder.DropColumn(
                name: "ProcessoId",
                table: "Cliente");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "172fd178-c088-4bb8-9f03-c798ecedb324", "13a70e2e-edbe-47e0-81dc-b37ea422e7a5", "Usuário", "USUÁRIO" },
                    { "a7454904-39e2-42cf-b83b-1dbc9a9452d0", "db88643f-a210-4b3a-bf83-bca5caae030c", "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "172fd178-c088-4bb8-9f03-c798ecedb324");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a7454904-39e2-42cf-b83b-1dbc9a9452d0");

            migrationBuilder.AddColumn<int>(
                name: "ProcessoId",
                table: "Cliente",
                type: "int",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 2);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "a06ed2d9-ad3e-4c8c-86af-6a0aba6a128b", "f6dda7a5-e5b6-457d-953d-7e0909dac105", "Usuário", "USUÁRIO" },
                    { "b508f537-ebfb-4621-ad2a-c2699e680a47", "7e9d6040-0a29-456b-a16f-f7e75e681563", "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_ProcessoId",
                table: "Cliente",
                column: "ProcessoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cliente_Processo_ProcessoId",
                table: "Cliente",
                column: "ProcessoId",
                principalTable: "Processo",
                principalColumn: "Id");
        }
    }
}
