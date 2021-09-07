using System;

namespace ERPBasico.Models
{
    public class Posicion : EntidadBase
    {
        public String nombre      { get; set; }
        public String descripcion { get; set; }
        public double sueldo      { get; set; }
        public Empleado empleado { get; set; }
        public Empleado Jefe { get; set; }
        public Gerencia gerencia { get; set; }
    }
}

