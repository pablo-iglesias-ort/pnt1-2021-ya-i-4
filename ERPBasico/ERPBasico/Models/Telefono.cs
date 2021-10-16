using System;

namespace ERPBasico.Models
{
    public class Telefono : EntidadBase
    {
        public int Numero { get; set; }
        public TipoTelefono Tipo { get; set; }
    }
}