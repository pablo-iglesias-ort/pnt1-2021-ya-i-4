using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPBasico.Migrations
{
    public partial class TelefonoFKEmpleado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Telefonos_Empleados_EmpleadoId",
                table: "Telefonos");

            migrationBuilder.AlterColumn<long>(
                name: "EmpleadoId",
                table: "Telefonos",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Telefonos_Empleados_EmpleadoId",
                table: "Telefonos",
                column: "EmpleadoId",
                principalTable: "Empleados",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Telefonos_Empleados_EmpleadoId",
                table: "Telefonos");

            migrationBuilder.AlterColumn<long>(
                name: "EmpleadoId",
                table: "Telefonos",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Telefonos_Empleados_EmpleadoId",
                table: "Telefonos",
                column: "EmpleadoId",
                principalTable: "Empleados",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
