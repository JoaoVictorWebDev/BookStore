using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStore.API.Migrations
{
    /// <inheritdoc />
    public partial class DTOCreationv3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Usuario",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "Usuario",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Senha",
                table: "Usuario",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "dataCriacao",
                table: "Usuario",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Categoria",
                table: "Livros",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataDePublicacao",
                table: "Livros",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Livros",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Quantidade",
                table: "Livros",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Titulo",
                table: "Livros",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataNascimento",
                table: "Autores",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "Autores",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Pais",
                table: "Autores",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "Nome",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "Senha",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "dataCriacao",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "Categoria",
                table: "Livros");

            migrationBuilder.DropColumn(
                name: "DataDePublicacao",
                table: "Livros");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Livros");

            migrationBuilder.DropColumn(
                name: "Quantidade",
                table: "Livros");

            migrationBuilder.DropColumn(
                name: "Titulo",
                table: "Livros");

            migrationBuilder.DropColumn(
                name: "DataNascimento",
                table: "Autores");

            migrationBuilder.DropColumn(
                name: "Nome",
                table: "Autores");

            migrationBuilder.DropColumn(
                name: "Pais",
                table: "Autores");
        }
    }
}
