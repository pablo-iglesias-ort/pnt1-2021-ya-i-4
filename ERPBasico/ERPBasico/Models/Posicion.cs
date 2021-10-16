using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERPBasico.Models
{
    public class Posicion : EntidadBase
    {
        [Required(ErrorMessage = "Nombre es un campo requerido")]
        public String nombre      { get; set; }
        public String descripcion { get; set; }
        public double sueldo      { get; set; }
        [ForeignKey("Empleado")]
        public long EmpleadoId { get; set; }
        public Empleado empleado { get; set; }
        [ForeignKey("Empleado")]
        public long JefeId { get; set; }
        public Empleado Jefe { get; set; }
        [ForeignKey("Gerencia")]
        public long GerenciaId { get; set; }
        public Gerencia gerencia { get; set; }
    }
}

