using System;

namespace ERPBasico.Models
{
    public class Empresa : EntidadBase
    {
        public string Nombre { get; set; }
        public Rubros rubro { get; set; }
        public Imagen Logo { get; set; }
        public string Direccion { get; set; }
        public Telefono TelefonoContacto { get; set; }
        public string EmailContacto { get; set; }

    }
}
