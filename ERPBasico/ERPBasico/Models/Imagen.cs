using System;

namespace ERPBasico.Models
{
    public class Imagen : EntidadBase
    {
        public String nombreImagen { get; set; }
        public byte[] imagen { get; set; }
    }
}
