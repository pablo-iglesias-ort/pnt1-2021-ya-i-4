using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ERPBasico.Models
{
    public abstract class EntidadBase
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public DateTime FechaAlta { get; set; }
    }
}
