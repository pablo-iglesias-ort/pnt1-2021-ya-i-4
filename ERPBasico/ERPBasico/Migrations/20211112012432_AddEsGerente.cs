using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPBasico.Migrations
{
    public partial class AddEsGerente : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Posiciones_GerenciaId",
                table: "Posiciones");

            migrationBuilder.AddColumn<bool>(
                name: "EsGerente",
                table: "Posiciones",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Posiciones_GerenciaId",
                table: "Posiciones",
                column: "GerenciaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Posiciones_GerenciaId",
                table: "Posiciones");

            migrationBuilder.DropColumn(
                name: "EsGerente",
                table: "Posiciones");

            migrationBuilder.CreateIndex(
                name: "IX_Posiciones_GerenciaId",
                table: "Posiciones",
                column: "GerenciaId",
                unique: true);
        }
    }
}
