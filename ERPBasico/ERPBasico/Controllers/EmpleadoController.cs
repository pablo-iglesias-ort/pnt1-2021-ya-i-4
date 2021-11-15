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
using System.Security.Claims;
using ERPBasico.Dtos;

namespace ERPBasico.Controllers
{
    [Authorize]
    public class EmpleadoController : Controller
    {
        private readonly ModelContext _context;
        private readonly ISeguridad _seguridad;

        public EmpleadoController(ModelContext context)
        {
            _context = context;
            _seguridad = new SeguridadBasica();
        }

        // GET: Empleadoes
        public async Task<IActionResult> Index()
        {
            var rol = User.FindFirst(ClaimTypes.Role).Value;
            if (rol == "EmpleadoRRHH")
                return View(await _context.Empleados.ToListAsync());
            else
            {
                var empleado = await _context.Empleados.FirstOrDefaultAsync(x => x.Id == ObtenerIdEmpleado());
                return View(nameof(Details), empleado);
            }
        }

        public async Task<IActionResult> EditarDatosContacto()
        {
            var IdEmpleado = ObtenerIdEmpleado();

            var empleadoConTelefonos = _context.Empleados
                                                    .Include(e => e.Telefonos)
                                                    .FirstOrDefault(e => e.Id == IdEmpleado);

            return View(empleadoConTelefonos);
        }

        // GET: Empleadoes/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleado = await (from p in _context.Posiciones
                                  join e in _context.Empleados on p.EmpleadoId equals e.Id
                                  join g in _context.Gerencias on p.GerenciaId equals g.Id
                                  where e.Id == id
                                  select new EmpleadoCompletoDto
                                  {
                                      Id = e.Id,
                                      Dni = e.Dni,
                                      Email = e.Email,
                                      Direccion = e.Direccion,
                                      Gerencia = g.Nombre,
                                      Legajo = e.Legajo,
                                      ObraSocial = e.ObraSocial,
                                      Posicion = p.nombre,
                                      NombreApellido = e.NombreApellido,
                                      EmpleadoActivo = e.EmpleadoActivo
                                  }).FirstOrDefaultAsync();
            if (empleado == null)
            {
                return NotFound();
            }

            return View(empleado);
        }

        public async Task<IActionResult> EditEmpleadoComun(Empleado empleado)
        {
            return View(empleado);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditEmpleadoComunForm([Bind("Direccion")] string direccion)
        {
            var id = ObtenerIdEmpleado();
            var empleado = await _context.Empleados.FirstOrDefaultAsync(x => x.Id == id);
            if (empleado == null)
                return NotFound();

            empleado.Direccion = direccion;
            _context.Update(empleado);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        // GET: Empleadoes/Create
        [Authorize(Roles = nameof(Rol.EmpleadoRRHH))]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Empleadoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = nameof(Rol.EmpleadoRRHH))]
        public async Task<IActionResult> Create([Bind("Apellido,Dni,Direccion,ObraSocial,Legajo,EmpleadoActivo,Nombre,Email,Id,FechaAlta")] Empleado empleado)
        {

            if (ModelState.IsValid)
            {
                empleado.Password = _seguridad.EncriptarPass(empleado.Dni.ToString());
                _context.Add(empleado);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(empleado);
        }

        // GET: Empleadoes/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleados.FindAsync(id);
            if (empleado == null)
            {
                return NotFound();
            }
            var rol = User.FindFirst(ClaimTypes.Role).Value;
            if (rol == "EmpleadoRRHH")
                return View(empleado);
            else
                return RedirectToAction("EditEmpleadoComun", empleado);
        }

        // POST: Empleadoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Apellido,Dni,Direccion,ObraSocial,Legajo,EmpleadoActivo,Nombre,Email,Id,FechaAlta")] Empleado empleado)
        {
            if (id != empleado.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(empleado);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmpleadoExists(empleado.Id))
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
            return View(empleado);
        }

        // GET: Empleadoes/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleados
                .FirstOrDefaultAsync(m => m.Id == id);
            if (empleado == null)
            {
                return NotFound();
            }

            return View(empleado);
        }

        // POST: Empleadoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var empleado = await _context.Empleados.FindAsync(id);
            _context.Empleados.Remove(empleado);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmpleadoExists(long id)
        {
            return _context.Empleados.Any(e => e.Id == id);
        }

        private long ObtenerIdEmpleado()
        {
            return long.Parse(User.FindFirst("EmpleadoId").Value);
        }
    }
}
