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
    public class TelefonosController : Controller
    {
        private readonly ModelContext _context;

        public TelefonosController(ModelContext context)
        {
            _context = context;
        }

        // GET: Telefonos
        public async Task<IActionResult> Index()
        {            
            return View(await _context.Telefonos.Where(x => x.EmpleadoId == ObtenerIdEmpleado()).ToListAsync());
        }

        // GET: Telefonos/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var telefono = await _context.Telefonos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (telefono == null)
            {
                return NotFound();
            }

            return View(telefono);
        }

        // GET: Telefonos/Create
        public IActionResult Create()
        {
            var tipoTelefonos = (from TipoTelefono t in Enum.GetValues(typeof(TipoTelefono))
                                 select new { Id = (long)t, Name = t.ToString() }).ToList();
            List<SelectListItem> items = tipoTelefonos.ConvertAll(t =>
            {
                return new SelectListItem()
                {
                    Text = t.Name,
                    Value = t.Id.ToString(),
                    Selected = false
                };
            });

            ViewBag.items = items;
            return View();
        }

        // POST: Telefonos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Numero,Tipo,Id")] Telefono telefono)
        {
            if (ModelState.IsValid)
            {
                telefono.FechaAlta = DateTime.Now;
                telefono.EmpleadoId = ObtenerIdEmpleado();
                _context.Add(telefono);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(telefono);
        }

        // GET: Telefonos/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var telefono = await _context.Telefonos.FindAsync(id);
            if (telefono == null)
            {
                return NotFound();
            }
            var tipoTelefonos = (from TipoTelefono t in Enum.GetValues(typeof(TipoTelefono))
                                select new { Id = (long)t, Name = t.ToString() }).ToList();
            List<SelectListItem> items = tipoTelefonos.ConvertAll(t =>
            {
                return new SelectListItem()
                {
                    Text = t.Name,
                    Value = t.Id.ToString(),
                    Selected = false
                };
            });

            ViewBag.items = items;
            return View(telefono);
        }

        // POST: Telefonos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Numero,Tipo,Id")] Telefono telefono)
        {
            if (id != telefono.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(telefono);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TelefonoExists(telefono.Id))
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
            return View(telefono);
        }

        // GET: Telefonos/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var telefono = await _context.Telefonos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (telefono == null)
            {
                return NotFound();
            }

            return View(telefono);
        }

        // POST: Telefonos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var telefono = await _context.Telefonos.FindAsync(id);
            _context.Telefonos.Remove(telefono);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TelefonoExists(long id)
        {
            return _context.Telefonos.Any(e => e.Id == id);
        }


        private long ObtenerIdEmpleado()
        {
            return long.Parse(User.FindFirst("EmpleadoId").Value);
        }
    }
}
