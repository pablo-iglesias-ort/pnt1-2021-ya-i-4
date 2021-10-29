using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPBasico.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CentrosDeCosto",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", nullable: true),
                    MontoMaximo = table.Column<double>(type: "REAL", nullable: false),
                    FechaAlta = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CentrosDeCosto", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Imagenes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    nombreImagen = table.Column<string>(type: "TEXT", nullable: true),
                    imagen = table.Column<byte[]>(type: "BLOB", nullable: true),
                    FechaAlta = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Imagenes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Empleados",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Apellido = table.Column<string>(type: "TEXT", maxLength: 15, nullable: false),
                    Dni = table.Column<int>(type: "INTEGER", nullable: false),
                    Direccion = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    ObraSocial = table.Column<string>(type: "TEXT", nullable: true),
                    Legajo = table.Column<uint>(type: "INTEGER", nullable: false),
                    EmpleadoActivo = table.Column<bool>(type: "INTEGER", nullable: false),
                    FotoId = table.Column<long>(type: "INTEGER", nullable: true),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    FechaAlta = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Nombre = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empleados", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Empleados_Imagenes_FotoId",
                        column: x => x.FotoId,
                        principalTable: "Imagenes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Gastos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Descripcion = table.Column<string>(type: "TEXT", nullable: true),
                    CentroDeCostoId = table.Column<long>(type: "INTEGER", nullable: false),
                    EmpleadoId = table.Column<long>(type: "INTEGER", nullable: false),
                    Monto = table.Column<double>(type: "REAL", nullable: false),
                    Fecha = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FechaAlta = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gastos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Gastos_CentrosDeCosto_CentroDeCostoId",
                        column: x => x.CentroDeCostoId,
                        principalTable: "CentrosDeCosto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Gastos_Empleados_EmpleadoId",
                        column: x => x.EmpleadoId,
                        principalTable: "Empleados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Telefonos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Numero = table.Column<int>(type: "INTEGER", nullable: false),
                    Tipo = table.Column<int>(type: "INTEGER", nullable: false),
                    EmpleadoId = table.Column<long>(type: "INTEGER", nullable: true),
                    FechaAlta = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Telefonos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Telefonos_Empleados_EmpleadoId",
                        column: x => x.EmpleadoId,
                        principalTable: "Empleados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Empresas",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", nullable: false),
                    rubro = table.Column<int>(type: "INTEGER", nullable: false),
                    LogoId = table.Column<long>(type: "INTEGER", nullable: true),
                    Direccion = table.Column<string>(type: "TEXT", nullable: false),
                    TelefonoContactoId = table.Column<long>(type: "INTEGER", nullable: true),
                    EmailContacto = table.Column<string>(type: "TEXT", nullable: false),
                    FechaAlta = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Empresas_Imagenes_LogoId",
                        column: x => x.LogoId,
                        principalTable: "Imagenes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Empresas_Telefonos_TelefonoContactoId",
                        column: x => x.TelefonoContactoId,
                        principalTable: "Telefonos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Gerencias",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", nullable: false),
                    EsGerenciaGeneral = table.Column<bool>(type: "INTEGER", nullable: false),
                    DireccionId = table.Column<long>(type: "INTEGER", nullable: true),
                    EmpresaId = table.Column<long>(type: "INTEGER", nullable: false),
                    FechaAlta = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gerencias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Gerencias_Empresas_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Gerencias_Gerencias_DireccionId",
                        column: x => x.DireccionId,
                        principalTable: "Gerencias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Posiciones",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    nombre = table.Column<string>(type: "TEXT", nullable: false),
                    descripcion = table.Column<string>(type: "TEXT", nullable: true),
                    sueldo = table.Column<double>(type: "REAL", nullable: false),
                    EmpleadoId = table.Column<long>(type: "INTEGER", nullable: false),
                    JefeId = table.Column<long>(type: "INTEGER", nullable: false),
                    GerenciaId = table.Column<long>(type: "INTEGER", nullable: false),
                    FechaAlta = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posiciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posiciones_Empleados_EmpleadoId",
                        column: x => x.EmpleadoId,
                        principalTable: "Empleados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Posiciones_Gerencias_GerenciaId",
                        column: x => x.GerenciaId,
                        principalTable: "Gerencias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Posiciones_Posiciones_JefeId",
                        column: x => x.JefeId,
                        principalTable: "Posiciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Empleados_FotoId",
                table: "Empleados",
                column: "FotoId");

            migrationBuilder.CreateIndex(
                name: "IX_Empresas_LogoId",
                table: "Empresas",
                column: "LogoId");

            migrationBuilder.CreateIndex(
                name: "IX_Empresas_TelefonoContactoId",
                table: "Empresas",
                column: "TelefonoContactoId");

            migrationBuilder.CreateIndex(
                name: "IX_Gastos_CentroDeCostoId",
                table: "Gastos",
                column: "CentroDeCostoId");

            migrationBuilder.CreateIndex(
                name: "IX_Gastos_EmpleadoId",
                table: "Gastos",
                column: "EmpleadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Gerencias_DireccionId",
                table: "Gerencias",
                column: "DireccionId");

            migrationBuilder.CreateIndex(
                name: "IX_Gerencias_EmpresaId",
                table: "Gerencias",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_Posiciones_EmpleadoId",
                table: "Posiciones",
                column: "EmpleadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Posiciones_GerenciaId",
                table: "Posiciones",
                column: "GerenciaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Posiciones_JefeId",
                table: "Posiciones",
                column: "JefeId");

            migrationBuilder.CreateIndex(
                name: "IX_Telefonos_EmpleadoId",
                table: "Telefonos",
                column: "EmpleadoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Gastos");

            migrationBuilder.DropTable(
                name: "Posiciones");

            migrationBuilder.DropTable(
                name: "CentrosDeCosto");

            migrationBuilder.DropTable(
                name: "Gerencias");

            migrationBuilder.DropTable(
                name: "Empresas");

            migrationBuilder.DropTable(
                name: "Telefonos");

            migrationBuilder.DropTable(
                name: "Empleados");

            migrationBuilder.DropTable(
                name: "Imagenes");
        }
    }
}
