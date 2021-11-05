using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ERPBasico.Controllers
{
    public class SeguridadBasica : ISeguridad
    {
        public byte[] EncriptarPass(string pass)
        {
            return new SHA256Managed().ComputeHash(Encoding.ASCII.GetBytes(pass));
        }
    }
}
