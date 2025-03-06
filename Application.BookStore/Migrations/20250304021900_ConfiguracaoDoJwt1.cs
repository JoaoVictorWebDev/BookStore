using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStore.API.Migrations
{
    /// <inheritdoc />
    public partial class ConfiguracaoDoJwt1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "DataCriacao", "Senha" },
                values: new object[] { new DateTime(2025, 3, 4, 2, 18, 59, 755, DateTimeKind.Utc).AddTicks(3359), "5F4DCC3B5AA765D61D8327DEB882CF99" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "DataCriacao", "Senha" },
                values: new object[] { new DateTime(2025, 3, 4, 2, 17, 21, 630, DateTimeKind.Utc).AddTicks(8191), "6258B113C62AA29D94ECF0E145E61F040CF4375A3058CD06E694311D48D17F57-3B8AA0EF6B5D4BBA64514B32AC90D30C" });
        }
    }
}
