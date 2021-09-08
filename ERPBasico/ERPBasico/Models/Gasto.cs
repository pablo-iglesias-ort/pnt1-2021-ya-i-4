using System;

namespace ERPBasico.Models
{
    public class Gasto : EntidadBase
    {
        public string Descripcion { get; set; }
        public CentroDeCosto CentroDeCosto { get; set; }
        public Empleado Empleado { get; set; }
        public double Monto { get; set; }
        public DateTime Fecha { get; set; }

    }
}
