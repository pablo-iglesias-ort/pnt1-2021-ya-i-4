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
        [Authorize(Roles = nameof(Rol.EmpleadoRRHH))]
        public IActionResult Create()
        {
            ViewData["GerenciaId"] = new SelectList(_context.Gerencias, "Id", "Nombre");
            return View();
        }

        // POST: Posicions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = nameof(Rol.EmpleadoRRHH))]
        public async Task<IActionResult> Create([Bind("nombre,descripcion,sueldo,GerenciaId")] Posicion posicion)
        {
            if (ModelState.IsValid)
            {
                posicion.FechaAlta = DateTime.Now;
                _context.Add(posicion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GerenciaId"] = new SelectList(_context.Gerencias, "Id", "Nombre", posicion.GerenciaId);
            return View(posicion);
        }
        // GET: Posicions/Edit/5
        [Authorize(Roles = nameof(Rol.EmpleadoRRHH))]
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
            var jefesElegibles = await _context.Posiciones.Where(pos => pos.GerenciaId == posicion.GerenciaId && pos.Id != posicion.Id).ToListAsync();
            ViewData["JefeId"] = new SelectList(jefesElegibles, "Id", "nombre", posicion.JefeId);
            ViewData["GerenciaId"] = new SelectList(_context.Gerencias, "Id", "Nombre", posicion.GerenciaId);
            TempData["GerenciaIdForm"] = posicion.GerenciaId.ToString();
            return View(posicion);
        }

        // POST: Posicions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = nameof(Rol.EmpleadoRRHH))]
        public async Task<IActionResult> Edit(long id, [Bind("nombre,descripcion,sueldo,JefeId,GerenciaId,Id")] Posicion posicion)
        {
            if (id != posicion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var gerenciaId = TempData["GerenciaIdForm"] as string;
                    posicion.GerenciaId = Convert.ToInt64(gerenciaId);
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
            var jefesElegibles = await _context.Posiciones.Where(pos => pos.GerenciaId == posicion.GerenciaId && pos.Id != posicion.Id).ToListAsync();
            ViewData["JefeId"] = new SelectList(jefesElegibles, "Id", "nombre", posicion.JefeId);
            ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "Id", "Apellido", posicion.EmpleadoId);
            ViewData["GerenciaId"] = new SelectList(_context.Gerencias, "Id", "Nombre", posicion.GerenciaId);
            return View(posicion);
        }

        // GET: Posicions/Delete/5
        [Authorize(Roles = nameof(Rol.EmpleadoRRHH))]
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
        [Authorize(Roles = nameof(Rol.EmpleadoRRHH))]
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

        [Authorize(Roles = nameof(Rol.EmpleadoRRHH))]
        public async Task<IActionResult> ListarGerencias(long idEmpleado)
        {
            ViewData["idEmpleado"] = idEmpleado;
            //ViewData["GerenciaId"] = new SelectList(_context.Gerencias, "Id", "Nombre");
            return View(await _context.Gerencias.ToListAsync());
        }

        [Authorize(Roles = nameof(Rol.EmpleadoRRHH))]
        public async Task<IActionResult> ListarPosiciones(long idEmpleado, long idGerencia)
        {
            ViewData["idEmpleado"] = idEmpleado;
            var posicionesElegibles = await _context.Posiciones.Where(pos => pos.GerenciaId == idGerencia).ToListAsync();
            return View(posicionesElegibles);
        }

        [Authorize(Roles = nameof(Rol.EmpleadoRRHH))]
        public async Task<IActionResult> AsignarPosicion(long idEmpleado, long idPosicion)
        {
            var posicion = await _context.Posiciones.FirstOrDefaultAsync(pos => pos.Id == idPosicion);
            posicion.EmpleadoId = idEmpleado;
            _context.Update(posicion);
            await _context.SaveChangesAsync();
            return View(nameof(Details), posicion);
        }
    }
}
