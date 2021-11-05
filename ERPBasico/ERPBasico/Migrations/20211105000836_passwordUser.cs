using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPBasico.Migrations
{
    public partial class passwordUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "Password",
                table: "Empleados",
                type: "BLOB",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Empleados",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(byte[]),
                oldType: "BLOB",
                oldNullable: true);
        }
    }
}
