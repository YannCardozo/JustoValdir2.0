using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class IncluindoChaveEstrangeiraDeClienteEmProcessoCorrigidaOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "709cc417-4e76-4e15-83e1-a616197dfdad");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d60bdd70-bcd9-4650-b527-1fe2a9203f89");

            migrationBuilder.AlterColumn<int>(
                name: "ClienteId",
                table: "Processo",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 2)
                .OldAnnotation("Relational:ColumnOrder", 1);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "a06ed2d9-ad3e-4c8c-86af-6a0aba6a128b", "f6dda7a5-e5b6-457d-953d-7e0909dac105", "Usuário", "USUÁRIO" },
                    { "b508f537-ebfb-4621-ad2a-c2699e680a47", "7e9d6040-0a29-456b-a16f-f7e75e681563", "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a06ed2d9-ad3e-4c8c-86af-6a0aba6a128b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b508f537-ebfb-4621-ad2a-c2699e680a47");

            migrationBuilder.AlterColumn<int>(
                name: "ClienteId",
                table: "Processo",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1)
                .OldAnnotation("Relational:ColumnOrder", 2);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "709cc417-4e76-4e15-83e1-a616197dfdad", "1f5951db-5c06-4de0-89c3-42a66333b1d5", "Usuário", "USUÁRIO" },
                    { "d60bdd70-bcd9-4650-b527-1fe2a9203f89", "ca8a5108-9709-4bf1-a47a-935248604bdd", "Admin", "ADMIN" }
                });
        }
    }
}
