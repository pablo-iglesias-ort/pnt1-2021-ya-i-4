using System;

namespace ERPBasico.Models
{
    public class Gerencia : EntidadBase
    {
        public String Nombre { get; set; }
        public bool EsGerenciaGeneral { get; set; }
        public Gerencia Direccion { get; set; }
        public Posicion Responsable { get; set; }
        public Posicion[] Posiciones{ get; set; }
        public Gerencia[] Gerencias { get; set; }
        public Empresa Empresa { get; set; }
    }
}
