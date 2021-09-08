using System;

namespace ERPBasico.Models
{
    public class CentroDeCosto : EntidadBase
    {
        public string Nombre { get; set; }
        public double MontoMaximo { get; set; }
        public Gasto[] Gastos { get; set; }
    }
}
