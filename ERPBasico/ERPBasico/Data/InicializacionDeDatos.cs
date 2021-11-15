using ERPBasico.Controllers;
using ERPBasico.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPBasico.Data
{
    public static class InicializacionDeDatos
    {
        private static readonly ISeguridad seguridad = new SeguridadBasica();
        private static readonly DateTime fechaHoy = DateTime.Now;

        public static void Inicializar(ModelContext context)
        {
            context.Database.EnsureCreated();

            if (!context.Empresas.Any())
            {
                var empresa = new Empresa
                {
                    Nombre = "Empresa IT",
                    Direccion = "Calle 123",
                    EmailContacto = "empresa@mail.com",
                    FechaAlta = fechaHoy
                };
                context.Empresas.Add(empresa);
                context.SaveChanges();
            }

            if (!context.Gerencias.Any())
            {
                var gerenciaGeneral = new Gerencia
                {
                    EmpresaId = 1,
                    EsGerenciaGeneral = true,
                    FechaAlta = fechaHoy,
                    Nombre = "Gerencia General IT"
                };
                var gerenciaIt = new Gerencia
                {
                    EmpresaId = 1,
                    FechaAlta = fechaHoy,
                    Nombre = "Gerencia IT",
                    EsGerenciaGeneral = false,
                };
                var gerenciaRRHH = new Gerencia
                {
                    EmpresaId = 1,
                    FechaAlta = fechaHoy,
                    EsGerenciaGeneral = false,
                    Nombre = "Gerencia de Recursos Humanos"
                };
                context.Gerencias.AddRange(gerenciaGeneral, gerenciaIt, gerenciaRRHH);
                context.SaveChanges();
            }

            if (!context.CentrosDeCosto.Any())
            {
                var ccGeneral = new CentroDeCosto
                {
                    Nombre = "Centro de costos de gerencia general",
                    MontoMaximo = 100000000,
                    GerenciaId = 1,
                    FechaAlta = fechaHoy
                };
                var ccIt = new CentroDeCosto
                {
                    Nombre = "Centro de costos de gerencia IT",
                    MontoMaximo = 266666,
                    GerenciaId = 2,
                    FechaAlta = fechaHoy
                };
                var ccRRHH = new CentroDeCosto
                {
                    Nombre = "Centro de costos de gerencia de RRHH",
                    MontoMaximo = 1868468,
                    GerenciaId = 3,
                    FechaAlta = fechaHoy
                };
                context.CentrosDeCosto.AddRange(ccGeneral, ccIt, ccRRHH);
                context.SaveChanges();
            }

            if (!context.Empleados.Any())
            {
                var dni = 12345678;
                var empleado1 = new Empleado
                {
                    Apellido = "Bonino",
                    Direccion = "calle 123",
                    Dni = dni,
                    Email = "bruno@mail.com",
                    EmpleadoActivo = true,
                    FechaAlta = fechaHoy,
                    Legajo = 1231,
                    Nombre = "Bruno",
                    ObraSocial = "OSDE",
                    Password = seguridad.EncriptarPass(dni.ToString()),
                    Rol = Rol.EmpleadoRRHH
                };
                var empleado2 = new Empleado
                {
                    Apellido = "Ampudia",
                    Direccion = "calle 123",
                    Dni = dni,
                    Email = "nacho@mail.com",
                    EmpleadoActivo = true,
                    FechaAlta = fechaHoy,
                    Legajo = 75681,
                    Nombre = "Ignacio",
                    ObraSocial = "OSDE",
                    Password = seguridad.EncriptarPass(dni.ToString()),
                    Rol = Rol.EmpleadoRRHH
                };
                var empleado3 = new Empleado
                {
                    Apellido = "Lombardo",
                    Direccion = "calle 123",
                    Dni = dni,
                    Email = "francisco@mail.com",
                    EmpleadoActivo = true,
                    FechaAlta = fechaHoy,
                    Legajo = 57138,
                    Nombre = "Francisco",
                    ObraSocial = "OSDE",
                    Password = seguridad.EncriptarPass(dni.ToString()),
                    Rol = Rol.EmpleadoRRHH
                };
                var empleado4 = new Empleado
                {
                    Apellido = "Redoni",
                    Direccion = "calle 123",
                    Dni = dni,
                    Email = "franco@mail.com",
                    EmpleadoActivo = true,
                    FechaAlta = fechaHoy,
                    Legajo = 1452,
                    Nombre = "Franco",
                    ObraSocial = "OSDE",
                    Password = seguridad.EncriptarPass(dni.ToString()),
                    Rol = Rol.EmpleadoRRHH
                };
                context.Empleados.AddRange(empleado1, empleado2, empleado3, empleado4);
                context.SaveChanges();
            }

            if (!context.Posiciones.Any())
            {
                var gerenteGeneral = new Posicion
                {
                    descripcion = "Gerente general",
                    EsGerente = true,
                    FechaAlta = fechaHoy,
                    GerenciaId = 1,
                    nombre = "Gerente General",
                    sueldo = 5638168
                };

                var gerenteIt = new Posicion
                {
                    descripcion = "Gerente IT",
                    EsGerente = true,
                    FechaAlta = fechaHoy,
                    GerenciaId = 2,
                    nombre = "Gerente IT",
                    sueldo = 56816,
                };

                var gerenteRRHH = new Posicion
                {
                    descripcion = "Gerente RRHH",
                    EsGerente = true,
                    FechaAlta = fechaHoy,
                    GerenciaId = 3,
                    nombre = "Gerente RRHH",
                    sueldo = 8541681
                };

                var desarrollador = new Posicion
                {
                    descripcion = "Desarrollador de aplicaciones",
                    EsGerente = false,
                    FechaAlta = fechaHoy,
                    GerenciaId = 2,
                    nombre = "Desarrollador",
                    sueldo = 15343
                };

                var administrativo = new Posicion
                {
                    descripcion = "Administrativo",
                    EsGerente = false,
                    FechaAlta = fechaHoy,
                    GerenciaId = 3,
                    nombre = "Administrativo",
                    sueldo = 15344
                };

                var liderProyecto = new Posicion
                {
                    descripcion = "Lider técnico de proyecto",
                    EsGerente = false,
                    FechaAlta = fechaHoy,
                    GerenciaId = 2,
                    nombre = "Lider de Proyecto",
                    sueldo = 15344
                };
                context.Posiciones.AddRange(gerenteGeneral, gerenteIt, gerenteRRHH, desarrollador, administrativo, liderProyecto);
                context.SaveChanges();
            }
        }
    }
}
