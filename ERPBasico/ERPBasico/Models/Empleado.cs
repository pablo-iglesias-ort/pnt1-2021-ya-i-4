using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPBasico.Models
{
    [Table("Empleados")]
    public class Empleado : PersonaBase
    {
        [Required(ErrorMessage = "Apellido es un campo requerido"), StringLength(15, ErrorMessage = "Apellido es demasiado largo")]
        public string Apellido { get; set; }
        [Required(ErrorMessage = "DNI es un campo requerido")]
        public int Dni { get; set; }        
        [MinLength(1, ErrorMessage = "Se debe registrar al menos un teléfono")]
        public virtual IList<Telefono> Telefonos { get; set; }
        //Posible entidad separada
        [Required, StringLength(20, ErrorMessage = "Dirección es un campo requerido")]
        public string Direccion { get; set; }
        //Posible entidad separada        
        public string ObraSocial { get; set; }
        [Required(ErrorMessage = "Legajo es un campo requerido")]
        public uint Legajo { get; set; }        
        public bool EmpleadoActivo { get; set; }
        [Required]
        public virtual Posicion Posicion { get; set; }
        public virtual Imagen Foto { get; set; }
    }
}
