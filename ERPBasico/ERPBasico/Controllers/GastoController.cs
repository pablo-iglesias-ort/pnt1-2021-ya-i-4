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
    public class GastoController : Controller
    {
        private readonly ModelContext _context;

        public GastoController(ModelContext context)
        {
            _context = context;
        }

        // GET: Gastos
        public async Task<IActionResult> Index()
        {
            var gastos = _context.Gastos.Where(x => x.EmpleadoId == ObtenerIdEmpleado());
            return View(await gastos.ToListAsync());
        }

        // GET: Gastos/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gasto = await _context.Gastos
                .Include(g => g.CentroDeCosto)
                .Include(g => g.Empleado)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gasto == null)
            {
                return NotFound();
            }

            return View(gasto);
        }

        // GET: Gastos/Create
        public async Task<IActionResult> Create()
        {
            var empleado = _context.Empleados.Where(x => x.Id == ObtenerIdEmpleado()).AsEnumerable();
            var centroDeCosto = await (from p in _context.Posiciones
                                       join g in _context.Gerencias on p.GerenciaId equals g.Id
                                       join e in _context.Empleados on p.EmpleadoId equals e.Id
                                       join c in _context.CentrosDeCosto on g.Id equals c.GerenciaId
                                       select c).ToListAsync();
            ViewData["Gerencia"] = new SelectList(centroDeCosto, "Id", "Nombre");
            ViewData["Empleado"] = new SelectList(empleado, "Id", "NombreApellido");
            return View();
        }

        // POST: Gastos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Descripcion,Monto,Fecha")] Gasto gasto)
        {
            
            if (ModelState.IsValid)
            {
                _context.Add(gasto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CentroDeCostoId"] = new SelectList(_context.CentrosDeCosto, "Id", "Id", gasto.CentroDeCostoId);
            ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "Id", "Apellido", gasto.EmpleadoId);
            return View(gasto);
        }

        // GET: Gastos/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gasto = await _context.Gastos.FindAsync(id);
            if (gasto == null)
            {
                return NotFound();
            }
            ViewData["CentroDeCostoId"] = new SelectList(_context.CentrosDeCosto, "Id", "Id", gasto.CentroDeCostoId);
            ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "Id", "Apellido", gasto.EmpleadoId);
            return View(gasto);
        }

        // POST: Gastos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Descripcion,CentroDeCostoId,EmpleadoId,Monto,Fecha,Id,FechaAlta")] Gasto gasto)
        {
            if (id != gasto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gasto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GastoExists(gasto.Id))
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
            ViewData["CentroDeCostoId"] = new SelectList(_context.CentrosDeCosto, "Id", "Id", gasto.CentroDeCostoId);
            ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "Id", "Apellido", gasto.EmpleadoId);
            return View(gasto);
        }

        // GET: Gastos/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gasto = await _context.Gastos
                .Include(g => g.CentroDeCosto)
                .Include(g => g.Empleado)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gasto == null)
            {
                return NotFound();
            }

            return View(gasto);
        }

        // POST: Gastos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var gasto = await _context.Gastos.FindAsync(id);
            _context.Gastos.Remove(gasto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GastoExists(long id)
        {
            return _context.Gastos.Any(e => e.Id == id);
        }

        private long ObtenerIdEmpleado()
        {
            return long.Parse(User.FindFirst("EmpleadoId").Value);
        }
    }
}
