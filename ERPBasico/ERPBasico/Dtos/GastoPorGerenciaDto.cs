using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPBasico.Dtos
{
    public class GastoPorGerenciaDto
    {
        public long Id { get; set; }
        public string Descripcion { get; set; }
        public string Empleado { get; set; }
        public string Gerencia { get; set; }
        public double Monto { get; set; }
        public string Fecha { get; set; }
    }
}
