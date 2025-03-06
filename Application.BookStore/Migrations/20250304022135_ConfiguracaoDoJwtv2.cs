using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStore.API.Migrations
{
    /// <inheritdoc />
    public partial class ConfiguracaoDoJwtv2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: 1L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Usuario",
                columns: new[] { "Id", "DataCriacao", "Email", "NomeDeUsuario", "Senha" },
                values: new object[] { 1L, new DateTime(2025, 3, 4, 2, 18, 59, 755, DateTimeKind.Utc).AddTicks(3359), "Administrador@hotmail.com", "Administrador", "5F4DCC3B5AA765D61D8327DEB882CF99" });
        }
    }
}
