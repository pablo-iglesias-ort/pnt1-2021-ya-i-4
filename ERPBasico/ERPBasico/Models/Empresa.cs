using System;

namespace ERPBasico.Models
{
    public class Empresa : EntidadBase
    {
        [Required(ErrorMessage = "Nombre es un campo requerido")]
        public string Nombre { get; set; }
        public Rubros rubro { get; set; }
        public Imagen Logo { get; set; }
        [range(4,100, ErrorManssage="Mail Invalido"),Required(ErrorMenssage= "La direccion es un campo requerido")]
        public string Direccion { get; set; }
        [Required(ErrorMenssage = "El telefono de contacto es un campo requerido")]
        public Telefono TelefonoContacto { get; set; }
        [Required(ErrorMenssage = "El mail de contacto es un campo requerido")]
        public string EmailContacto { get; set; }

    }
}
