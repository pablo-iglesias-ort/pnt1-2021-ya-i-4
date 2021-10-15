using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace ERPBasico.Models
{
    public class Empresa : EntidadBase
    {
        [Required(ErrorMessage = "Nombre es un campo requerido")]
        public string Nombre { get; set; }
        public Rubros rubro { get; set; }
        public Imagen Logo { get; set; }
        [Range(4,100, ErrorMessage="Mail Invalido"),Required(ErrorMessage= "La direccion es un campo requerido")]
        public string Direccion { get; set; }
        [Required(ErrorMessage = "El telefono de contacto es un campo requerido")]
        public Telefono TelefonoContacto { get; set; }
        [Required(ErrorMessage = "El mail de contacto es un campo requerido")]
        public string EmailContacto { get; set; }

    }
}
