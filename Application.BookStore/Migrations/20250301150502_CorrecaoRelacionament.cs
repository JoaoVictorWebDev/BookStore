using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStore.API.Migrations
{
    /// <inheritdoc />
    public partial class CorrecaoRelacionament : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "Usuario",
                newName: "NomeDeUsuario");

            migrationBuilder.AlterColumn<string>(
                name: "Senha",
                table: "Usuario",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Usuario",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "NomeDeUsuario",
                table: "Usuario",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Titulo",
                table: "Livros",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<long>(
                name: "AutorID",
                table: "Livros",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<string>(
                name: "Pais",
                table: "Autores",
                type: "character varying(180)",
                maxLength: 180,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Autores",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateIndex(
                name: "IX_Livros_AutorID",
                table: "Livros",
                column: "AutorID");

            migrationBuilder.AddForeignKey(
                name: "FK_Livros_Autores_AutorID",
                table: "Livros",
                column: "AutorID",
                principalTable: "Autores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Livros_Autores_AutorID",
                table: "Livros");

            migrationBuilder.DropIndex(
                name: "IX_Livros_AutorID",
                table: "Livros");

            migrationBuilder.DropColumn(
                name: "AutorID",
                table: "Livros");

            migrationBuilder.RenameColumn(
                name: "NomeDeUsuario",
                table: "Usuario",
                newName: "Nome");

            migrationBuilder.AlterColumn<string>(
                name: "Senha",
                table: "Usuario",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Usuario",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Usuario",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Titulo",
                table: "Livros",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Pais",
                table: "Autores",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(180)",
                oldMaxLength: 180);

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Autores",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255);
        }
    }
}
