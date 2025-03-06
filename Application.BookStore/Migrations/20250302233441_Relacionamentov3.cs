using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BookStore.API.Migrations
{
    /// <inheritdoc />
    public partial class Relacionamentov3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Categoria",
                table: "Livros");

            migrationBuilder.AddColumn<long>(
                name: "CategoriaID",
                table: "Livros",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "Categoria",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoria", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Livros_CategoriaID",
                table: "Livros",
                column: "CategoriaID");

            migrationBuilder.AddForeignKey(
                name: "FK_Livros_Categoria_CategoriaID",
                table: "Livros",
                column: "CategoriaID",
                principalTable: "Categoria",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Livros_Categoria_CategoriaID",
                table: "Livros");

            migrationBuilder.DropTable(
                name: "Categoria");

            migrationBuilder.DropIndex(
                name: "IX_Livros_CategoriaID",
                table: "Livros");

            migrationBuilder.DropColumn(
                name: "CategoriaID",
                table: "Livros");

            migrationBuilder.AddColumn<int>(
                name: "Categoria",
                table: "Livros",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
