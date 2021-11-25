using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ERPBasico.Data;
using ERPBasico.Models;
using ERPBasico.Dtos;
using Microsoft.AspNetCore.Authorization;

namespace ERPBasico.Controllers
{
    [Authorize]
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
            var gastos = _context.Gastos.Where(x => x.EmpleadoId == ObtenerIdEmpleado()).OrderByDescending(x => x.Fecha);
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
            var empleadoId = ObtenerIdEmpleado();
            var empleadoDto = await (from p in _context.Posiciones
                                     join g in _context.Gerencias on p.GerenciaId equals g.Id
                                     join e in _context.Empleados on p.EmpleadoId equals e.Id
                                     join c in _context.CentrosDeCosto on g.Id equals c.GerenciaId
                                     where p.EmpleadoId == empleadoId
                                     select new EmpleadoCompletoDto
                                     {
                                         NombreApellido = e.NombreApellido,
                                         Dni = e.Dni,
                                         NombreGerencia = g.Nombre,
                                         NombreCentroDeCostos = c.Nombre,
                                         NombrePosicion = p.nombre,
                                         GerenciaId = g.Id,
                                         CCId = c.Id,
                                         PosicionId = p.Id,
                                         MontoMaximoCC = c.MontoMaximo
                                     }).ToListAsync();
            var empleado = empleadoDto.FirstOrDefault();
            empleado.MontoDisponible = await ObtenerMontoDisponiblePorCC(empleado.CCId, empleado.MontoMaximoCC);
            TempData["GastoForm"] = String.Concat(empleado.CCId, ",", empleado.MontoDisponible.ToString());
            ViewData["Gerencia"] = new SelectList(empleadoDto, "Dni", "NombreGerencia");
            ViewData["Empleado"] = new SelectList(empleadoDto, "Dni", "NombreApellido");
            ViewData["MontoDisp"] = new SelectList(empleadoDto, "Dni", "MontoDisponible");
            return View();
        }

        // POST: Gastos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Descripcion,Monto,Fecha")] Gasto gasto)
        {
            var gastoForm = TempData["GastoForm"] as string;
            var ccIdMontoArray = gastoForm.Split(',');
            var ccId = Convert.ToInt64(ccIdMontoArray[0]);
            var montoMaximoDisponible = Convert.ToDouble(ccIdMontoArray[1]);

            if (ModelState.IsValid)
            {
                using (var tran = _context.Database.BeginTransaction())
                {
                    try
                    {
                        if (gasto.Monto > montoMaximoDisponible)
                        {
                            return BadRequest();
                        }
                        gasto.CentroDeCostoId = ccId;
                        gasto.EmpleadoId = ObtenerIdEmpleado();
                        gasto.FechaAlta = DateTime.Now;
                        _context.Gastos.Add(gasto);
                        await _context.SaveChangesAsync();
                        var cc = await _context.CentrosDeCosto.FirstOrDefaultAsync(x => x.Id == gasto.CentroDeCostoId);
                        cc.MontoMaximo -= gasto.Monto;
                        _context.CentrosDeCosto.Update(cc);
                        await _context.SaveChangesAsync();
                        tran.Commit();
                    }
                    catch (Exception e)
                    {
                        tran.Rollback();
                        throw e;
                    }
                }
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

        [Authorize(Roles = nameof(Rol.EmpleadoRRHH))]
        public async Task<IActionResult> ListarGastosPorGerencia(long gerenciaId)
        {
            var gastos = await (from g in _context.Gastos
                         join e in _context.Empleados on g.EmpleadoId equals e.Id into empleados
                         from emp in empleados.DefaultIfEmpty()
                         join c in _context.CentrosDeCosto on g.CentroDeCostoId equals c.Id into centrosDeCosto
                         from cc in centrosDeCosto.DefaultIfEmpty()
                         join ge in _context.Gerencias on cc.GerenciaId equals ge.Id into gerencias
                         from ger in gerencias.DefaultIfEmpty()
                         where ger.Id == gerenciaId
                         select new GastoPorGerenciaDto
                         {
                             Id = g.Id,
                             Descripcion = g.Descripcion,
                             Empleado = String.IsNullOrEmpty(emp.NombreApellido) ? string.Empty : emp.NombreApellido,
                             Fecha = g.Fecha.ToString("dd-MM-yyyy"),
                             Gerencia = String.IsNullOrEmpty(ger.Nombre) ? string.Empty : ger.Nombre,
                             Monto = g.Monto
                         }).ToListAsync();

            return View(gastos);
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



        private async Task<double> ObtenerMontoDisponiblePorCC(long cCId, double montoMaximoCC)
        {
            var gastosPorCC = await _context.Gastos.Where(x => x.CentroDeCostoId == cCId).ToListAsync();
            double gastoAlMomento = 0;
            foreach (var gasto in gastosPorCC)
            {
                gastoAlMomento += gasto.Monto;
            }
            return montoMaximoCC - gastoAlMomento;
        }
    }
}
