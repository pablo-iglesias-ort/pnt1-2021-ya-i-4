using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPBasico.Models
{
    public class Telefono : EntidadBase
    {
        public int Numero { get; set; }
        public TipoTelefono TipoTelefono { get; set; }
    }
}
