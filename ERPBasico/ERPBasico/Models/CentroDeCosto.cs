using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERPBasico.Models
{
    public class CentroDeCosto : EntidadBase
    {
        public string Nombre { get; set; }
        public double MontoMaximo { get; set; }
        [ForeignKey("Gerencia")]
        public long GerenciaId { get; set; }
        public virtual Gerencia Gerencia { get; set; }
        public Gasto[] Gastos { get; set; }
    }
}
