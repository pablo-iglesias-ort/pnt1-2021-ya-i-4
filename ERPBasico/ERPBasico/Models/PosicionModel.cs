using System;

namespace ERPBasico.Models
{
    public class PosicionModel
    {
        public String nombre      { get; set; };
        public String descripcion { get; set; };
        public double sueldo      { get; set; };
        public EmpleadoModel empleado { get; set; };
        public EmpleadoModel Jefe { get; set; };
        public GerenciaModel gerencia { get; set; };
    }
}

