using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERPBasico.Models
{
    public class Telefono : EntidadBase
    {
        [ForeignKey("Empleado")]
        public long EmpleadoId { get; set; }
        public Empleado Empleado { get; set; }
        public int Numero { get; set; }
        public TipoTelefono Tipo { get; set; }
    }
}