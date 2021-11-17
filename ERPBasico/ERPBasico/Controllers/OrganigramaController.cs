using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ERPBasico.Data;
using ERPBasico.Models;
using Microsoft.AspNetCore.Authorization;

namespace ERPBasico.Controllers
{
    [Authorize]
    public class OrganigramaController : Controller
    {
        private readonly ModelContext _context;
        public OrganigramaController(ModelContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var gerenciaGeneral = await GetGerenciaGeneral();
            var listaSubgerencias = await GetSubgerenciasById(gerenciaGeneral.Id);
            return View("ListaSubgerencias", listaSubgerencias);
        }

        public async Task<IActionResult> AbrirGerencia(long id)
        {
            var listaSubgerencias = await GetSubgerenciasById(id);
            if(listaSubgerencias.Count != 0)
            {
                return View("ListaSubgerencias", listaSubgerencias);
            }
            var listaPosiciones = await GetPosicionesByIdGerencia(id);
            return View("ListaPosiciones", listaPosiciones);
        }

        private async Task<List<Gerencia>> GetSubgerenciasById(long idGerencia)
        {
            return await _context.Gerencias.Where(ger => ger.DireccionId == idGerencia).ToListAsync();
        }
        private async Task<Gerencia> GetGerenciaGeneral()
        {
            return await _context.Gerencias.Where(ger => ger.EsGerenciaGeneral).FirstOrDefaultAsync();
        }
        private async Task<List<Posicion>> GetPosicionesByIdGerencia(long id)
        {
            return await _context.Posiciones.Where(pos => pos.GerenciaId == id).ToListAsync();
        }
    }
}
