using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPBasico.Models
{
    public class Gerencia : EntidadBase
    {
        [Required(ErrorMessage = "Nombre es un campo requerido")]
        public String Nombre { get; set; }
        public bool EsGerenciaGeneral { get; set; }
        [Required, StringLength(20, ErrorMessage = "Dirección es un campo requerido")]
        public Gerencia Direccion { get; set; }
        public Posicion Responsable { get; set; }
        public Posicion[] Posiciones{ get; set; }
        public Gerencia[] Gerencias { get; set; }
        public Empresa Empresa { get; set; }
    }
}
