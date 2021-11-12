using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERPBasico.Models
{
    public class Gerencia : EntidadBase
    {
        [Required(ErrorMessage = "Nombre es un campo requerido")]
        public String Nombre { get; set; }
        public bool EsGerenciaGeneral { get; set; }
        public long? DireccionId { get; set; }
        public Gerencia Direccion { get; set; }
        [ForeignKey("Empresa")]
        public long EmpresaId { get; set; }
        public Empresa Empresa { get; set; }
    }
}
