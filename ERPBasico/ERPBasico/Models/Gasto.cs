using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERPBasico.Models
{
    public class Gasto : EntidadBase
    {
        public string Descripcion { get; set; }
        [ForeignKey("CentroDeCosto")]
        public long CentroDeCostoId { get; set; }
        public CentroDeCosto CentroDeCosto { get; set; }
        [ForeignKey("Empleado")]
        public long EmpleadoId { get; set; }
        public Empleado Empleado { get; set; }
        public double Monto { get; set; }
        public DateTime Fecha { get; set; }

    }
}
