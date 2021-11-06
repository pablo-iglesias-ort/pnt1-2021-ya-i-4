using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPBasico.Migrations
{
    public partial class CampoGerenciaACentroDeCosto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "GerenciaId",
                table: "CentrosDeCosto",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_CentrosDeCosto_GerenciaId",
                table: "CentrosDeCosto",
                column: "GerenciaId");

            migrationBuilder.AddForeignKey(
                name: "FK_CentrosDeCosto_Gerencias_GerenciaId",
                table: "CentrosDeCosto",
                column: "GerenciaId",
                principalTable: "Gerencias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CentrosDeCosto_Gerencias_GerenciaId",
                table: "CentrosDeCosto");

            migrationBuilder.DropIndex(
                name: "IX_CentrosDeCosto_GerenciaId",
                table: "CentrosDeCosto");

            migrationBuilder.DropColumn(
                name: "GerenciaId",
                table: "CentrosDeCosto");
        }
    }
}
