using ERPBasico.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPBasico.Dtos
{
    public class GastosPorCCDto
    {
        public double MontoMaximoCC { get; set; }
        public List<Gasto> Gastos { get; set; }
    }
}
