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
            //var modelContext = _context.Gerencias.Include(g => g.Empresa);
            //var modelList = await modelContext.ToListAsync();
            var modelContext = await _context.Gerencias.ToListAsync();
            return View(modelContext);
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
        [Authorize(Roles = nameof(Rol.EmpleadoRRHH))]
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
        [Authorize(Roles = nameof(Rol.EmpleadoRRHH))]
        public async Task<IActionResult> Create([Bind("Nombre,DireccionId")] Gerencia gerencia)
        {
            if (ModelState.IsValid)
            {

                using (var transac = _context.Database.BeginTransaction())
                {
                    try
                    {
                        gerencia.FechaAlta = DateTime.Now;
                        gerencia.EmpresaId = 1;
                        _context.Add(gerencia);
                        await _context.SaveChangesAsync();
                        var gerente = new Posicion
                        {
                            nombre = $@"Gerente de {gerencia.Nombre}",
                            GerenciaId = gerencia.Id,
                            gerencia = gerencia,
                            FechaAlta= DateTime.Now,
                            EsGerente= true
                        };
                        //_context.Add(gerencia);
                        _context.Posiciones.Add(gerente);
                        await _context.SaveChangesAsync();
                        transac.Commit();
                    }
                    catch(Exception e)
                    {
                        transac.Rollback();
                        throw e;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Gerencias"] = new SelectList(_context.Gerencias, "Id");
            return View(gerencia);
        }

        // GET: Gerencias/Edit/5
        [Authorize(Roles = nameof(Rol.EmpleadoRRHH))]
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

            var gerenciasElegibles = await _context.Gerencias.Where(ger => ger.Id != id).ToListAsync();
            ViewData["Gerencias"] = new SelectList(gerenciasElegibles, "Id", "Nombre", gerencia.Direccion);
            return View(gerencia);
        }

        // POST: Gerencias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = nameof(Rol.EmpleadoRRHH))]
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
        [Authorize(Roles = nameof(Rol.EmpleadoRRHH))]
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
        [Authorize(Roles = nameof(Rol.EmpleadoRRHH))]
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
