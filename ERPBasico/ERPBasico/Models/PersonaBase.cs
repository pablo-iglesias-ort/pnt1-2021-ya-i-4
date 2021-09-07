using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPBasico.Models
{
    public abstract class PersonaBase : EntidadBase
    {
        [Required(ErrorMessage = "Nombre es un campo requerido"), StringLength(30, ErrorMessage = "Nombre demasiado largo")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Email es un campo requerido")]
        [EmailAddress]
        public string Email { get; set; }
    }
}
