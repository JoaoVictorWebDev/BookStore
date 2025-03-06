using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStore.API.Migrations
{
    /// <inheritdoc />
    public partial class ConfiguracaoDoJwt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "dataCriacao",
                table: "Usuario",
                newName: "DataCriacao");

            migrationBuilder.InsertData(
                table: "Usuario",
                columns: new[] { "Id", "DataCriacao", "Email", "NomeDeUsuario", "Senha" },
                values: new object[] { 1L, new DateTime(2025, 3, 4, 2, 17, 21, 630, DateTimeKind.Utc).AddTicks(8191), "Administrador@hotmail.com", "Administrador", "6258B113C62AA29D94ECF0E145E61F040CF4375A3058CD06E694311D48D17F57-3B8AA0EF6B5D4BBA64514B32AC90D30C" });

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_Email",
                table: "Usuario",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_NomeDeUsuario",
                table: "Usuario",
                column: "NomeDeUsuario",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Usuario_Email",
                table: "Usuario");

            migrationBuilder.DropIndex(
                name: "IX_Usuario_NomeDeUsuario",
                table: "Usuario");

            migrationBuilder.DeleteData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.RenameColumn(
                name: "DataCriacao",
                table: "Usuario",
                newName: "dataCriacao");
        }
    }
}
