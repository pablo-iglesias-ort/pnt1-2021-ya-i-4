using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPBasico.Controllers
{
    public interface ISeguridad
    {
        public byte[] EncriptarPass(string pass);
        
    }

 
}
