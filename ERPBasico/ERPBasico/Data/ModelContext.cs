using ERPBasico.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPBasico.Dtos;

namespace ERPBasico.Data
{
    public class ModelContext : DbContext
    {
        public ModelContext(DbContextOptions<ModelContext> opt) : base(opt) { }
        public DbSet<CentroDeCosto> CentrosDeCosto { get; set; }
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Gasto> Gastos { get; set; }
        public DbSet<Gerencia> Gerencias { get; set; }
        public DbSet<Imagen> Imagenes { get; set; }
        public DbSet<Posicion> Posiciones { get; set; }
        public DbSet<Telefono> Telefonos { get; set; }

    }
}
