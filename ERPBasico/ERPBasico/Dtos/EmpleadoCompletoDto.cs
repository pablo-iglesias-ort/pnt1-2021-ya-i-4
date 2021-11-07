using ERPBasico.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPBasico.Dtos
{
    public class EmpleadoCompletoDto
    {
        public string NombreApellido { get; set; }
        public int Dni { get; set; }
        public long GerenciaId { get; set; }
        public long PosicionId { get; set; }
        public long CCId { get; set; }
        public string NombreCentroDeCostos { get; set; }
        public string NombrePosicion { get; set; }
        public string NombreGerencia { get; set; }
        public double MontoDisponible { get; set; }
        public double MontoMaximoCC { get; set; }
    }
}
