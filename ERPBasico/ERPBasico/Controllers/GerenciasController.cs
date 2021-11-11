using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ERPBasico.Data;
using ERPBasico.Models;

namespace ERPBasico.Controllers
{
    public class GerenciasController : Controller
    {
        private readonly ModelContext _context;

        public GerenciasController(ModelContext context)
        {
            _context = context;
        }

        // GET: Gerencias
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Gerencias.Include(g => g.Empresa);
            return View(await modelContext.ToListAsync());
        }

        // GET: Gerencias/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gerencia = await _context.Gerencias
                .Include(g => g.Empresa)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gerencia == null)
            {
                return NotFound();
            }

            return View(gerencia);
        }

        // GET: Gerencias/Create
        public IActionResult Create()
        {
            ViewData["Gerencias"] = new SelectList(_context.Gerencias, "Id", "Nombre");
            return View();
        }

        // POST: Gerencias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nombre,EsGerenciaGeneral,EmpresaId,Id,FechaAlta,Direccion")] Gerencia gerencia)
        {
            if (ModelState.IsValid)
            {
                var gerente = new Posicion
                {
                    nombre = $@"Gerente de {gerencia.Nombre}",
                    GerenciaId= gerencia.Id,
                    gerencia= gerencia
                };
                _context.Posiciones.Add(gerente);
                _context.Add(gerencia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Gerencias"] = new SelectList(_context.Gerencias, "Id");
            return View(gerencia);
        }

        // GET: Gerencias/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gerencia = await _context.Gerencias.FindAsync(id);
            if (gerencia == null)
            {
                return NotFound();
            }
            ViewData["Gerencias"] = new SelectList(_context.Gerencias, "Id", "Nombre", gerencia.Direccion);
            return View(gerencia);
        }

        // POST: Gerencias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Nombre,EsGerenciaGeneral,EmpresaId,Id,FechaAlta")] Gerencia gerencia)
        {
            if (id != gerencia.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gerencia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GerenciaExists(gerencia.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmpresaId"] = new SelectList(_context.Empresas, "Id", "Direccion", gerencia.EmpresaId);
            return View(gerencia);
        }

        // GET: Gerencias/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gerencia = await _context.Gerencias
                .Include(g => g.Empresa)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gerencia == null)
            {
                return NotFound();
            }

            return View(gerencia);
        }

        // POST: Gerencias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var gerencia = await _context.Gerencias.FindAsync(id);
            _context.Gerencias.Remove(gerencia);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GerenciaExists(long id)
        {
            return _context.Gerencias.Any(e => e.Id == id);
        }
    }
}
