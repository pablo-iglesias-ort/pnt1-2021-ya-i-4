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
    public class PosicionsController : Controller
    {
        private readonly ModelContext _context;

        public PosicionsController(ModelContext context)
        {
            _context = context;
        }

        // GET: Posicions
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Posiciones.Include(p => p.Jefe).Include(p => p.empleado).Include(p => p.gerencia);
            return View(await modelContext.ToListAsync());
        }

        // GET: Posicions/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var posicion = await _context.Posiciones
                .Include(p => p.Jefe)
                .Include(p => p.empleado)
                .Include(p => p.gerencia)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (posicion == null)
            {
                return NotFound();
            }

            return View(posicion);
        }

        // GET: Posicions/Create
        public IActionResult Create()
        {
            ViewData["JefeId"] = new SelectList(_context.Posiciones, "Id", "nombre");
            ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "Id", "Apellido");
            ViewData["GerenciaId"] = new SelectList(_context.Gerencias, "Id", "Nombre");
            return View();
        }

        // POST: Posicions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("nombre,descripcion,sueldo,EmpleadoId,JefeId,GerenciaId,Id,FechaAlta")] Posicion posicion)
        {
            var jefeValido = await JefePerteneceAGerenciaByPosicion(posicion);
            if (ModelState.IsValid && jefeValido)
            {
                _context.Add(posicion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["JefeId"] = new SelectList(_context.Posiciones, "Id", "nombre", posicion.JefeId);
            ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "Id", "Apellido", posicion.EmpleadoId);
            ViewData["GerenciaId"] = new SelectList(_context.Gerencias, "Id", "Nombre", posicion.GerenciaId);
            return View(posicion);
        }

        private async Task<Boolean> JefePerteneceAGerenciaByPosicion(Posicion posicion)
        {
            var jefe = await _context.Posiciones.FindAsync(posicion.JefeId);
            return jefe.GerenciaId == posicion.GerenciaId && posicion.Id != posicion.JefeId;
        }
        // GET: Posicions/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var posicion = await _context.Posiciones.FindAsync(id);
            if (posicion == null)
            {
                return NotFound();
            }
            ViewData["JefeId"] = new SelectList(_context.Posiciones, "Id", "nombre", posicion.JefeId);
            ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "Id", "Apellido", posicion.EmpleadoId);
            ViewData["GerenciaId"] = new SelectList(_context.Gerencias, "Id", "Nombre", posicion.GerenciaId);
            return View(posicion);
        }

        // POST: Posicions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("nombre,descripcion,sueldo,EmpleadoId,JefeId,GerenciaId,Id,FechaAlta")] Posicion posicion)
        {
            if (id != posicion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(posicion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PosicionExists(posicion.Id))
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
            ViewData["JefeId"] = new SelectList(_context.Posiciones, "Id", "nombre", posicion.JefeId);
            ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "Id", "Apellido", posicion.EmpleadoId);
            ViewData["GerenciaId"] = new SelectList(_context.Gerencias, "Id", "Nombre", posicion.GerenciaId);
            return View(posicion);
        }

        // GET: Posicions/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var posicion = await _context.Posiciones
                .Include(p => p.Jefe)
                .Include(p => p.empleado)
                .Include(p => p.gerencia)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (posicion == null)
            {
                return NotFound();
            }

            return View(posicion);
        }

        // POST: Posicions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var posicion = await _context.Posiciones.FindAsync(id);
            _context.Posiciones.Remove(posicion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PosicionExists(long id)
        {
            return _context.Posiciones.Any(e => e.Id == id);
        }
    }
}
