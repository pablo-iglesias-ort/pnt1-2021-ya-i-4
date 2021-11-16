using ERPBasico.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPBasico.Dtos
{
    public class EmpleadoCompletoDto
    {
        public long Id { get; set; }
        public string NombreApellido { get; set; }
        public int Dni { get; set; }
        public long GerenciaId { get; set; }
        public long PosicionId { get; set; }
        public long CCId { get; set; }
        public string Sueldo { get; set; }
        public string NombreCentroDeCostos { get; set; }
        public string NombrePosicion { get; set; }
        public string NombreGerencia { get; set; }
        public double MontoDisponible { get; set; }
        public double MontoMaximoCC { get; set; }
        public string Posicion { get; set; }
        public string Direccion { get; set; }
        public string ObraSocial { get; set; }
        public uint Legajo { get; set; }
        public bool EmpleadoActivo { get; set; }
        public string Email { get; set; }
        public string Gerencia { get; set; }
    }
}
