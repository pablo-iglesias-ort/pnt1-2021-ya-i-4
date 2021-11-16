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
            if (EsEmpleadoRRHH())
            {
                List<EmpleadoCompletoDto> empleados = await ObtenerEmpleadosParaVisualizar();
                return View(empleados.OrderByDescending(x => x.Sueldo));
            }
            else
            {
                var empleado = await ObtenerEmpleadoDetails(ObtenerIdEmpleado());
                return View(nameof(Details), empleado);
            }
        }

        private async Task<List<EmpleadoCompletoDto>> ObtenerEmpleadosParaVisualizar()
        {
            //List<EmpleadoCompletoDto> empleados = new List<EmpleadoCompletoDto>();
            var empleados = from e in _context.Empleados
                        join p in _context.Posiciones on e.Id equals p.EmpleadoId into posiciones
                        from po in posiciones.DefaultIfEmpty()
                        join g in _context.Gerencias on po.GerenciaId equals g.Id into gerencias
                        from ge in gerencias.DefaultIfEmpty()
                        select new EmpleadoCompletoDto
                        {
                            Id = e.Id,
                            Sueldo = po == null ? "Sin sueldo asignado" : po.sueldo.ToString(),
                            Gerencia = String.IsNullOrEmpty(ge.Nombre) ? "Sin gerencia" : ge.Nombre,
                            Posicion = String.IsNullOrEmpty(po.nombre) ? "Sin posición" : po.nombre,
                            NombreApellido = e.NombreApellido,
                            EmpleadoActivo = e.EmpleadoActivo
                        };

            if(empleados == null)
            {
                return new List<EmpleadoCompletoDto>();
            }

            return await empleados.ToListAsync();
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

            var empleado = await ObtenerEmpleadoDetails((long)id);

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
        public async Task<IActionResult> Create([Bind("Apellido,Dni,Direccion,ObraSocial,Legajo,EmpleadoActivo,Nombre,Email,Id, EsRRHH")] Empleado empleado)
        {

            if (ModelState.IsValid)
            {
                empleado.FechaAlta = DateTime.Now;
                empleado.Rol = empleado.EsRRHH ? Rol.EmpleadoRRHH : Rol.Empleado;
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
            empleado.EsRRHH = empleado.Rol == Rol.EmpleadoRRHH ? true : false;
            if (empleado == null)
            {
                return NotFound();
            }

            if (EsEmpleadoRRHH())
                return View(empleado);
            else
                return RedirectToAction(nameof(EditEmpleadoComun), empleado);
        }

        // POST: Empleadoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Apellido,Dni,Direccion,ObraSocial,Legajo,EmpleadoActivo,Nombre,Email,Id, EsRRHH")] Empleado empleado)
        {
            if (id != empleado.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    empleado.Rol = empleado.EsRRHH ? Rol.EmpleadoRRHH : Rol.Empleado;
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
            empleado.EmpleadoActivo = false;
            _context.Update(empleado);
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

        private async Task<EmpleadoCompletoDto> ObtenerEmpleadoDetails(long id)
        {
            var empleado = await (from e in _context.Empleados
                                  join p in _context.Posiciones on e.Id equals p.EmpleadoId into posiciones
                                  from po in posiciones.DefaultIfEmpty()
                                  join g in _context.Gerencias on po.GerenciaId equals g.Id into gerencias
                                  from ge in gerencias.DefaultIfEmpty()
                                  where e.Id == id
                                  select new EmpleadoCompletoDto
                                  {
                                      Id = e.Id,
                                      Dni = e.Dni,
                                      Email = e.Email,
                                      Direccion = e.Direccion,
                                      Gerencia = String.IsNullOrEmpty(ge.Nombre) ? "Sin gerencia" : ge.Nombre,
                                      Legajo = e.Legajo,
                                      ObraSocial = e.ObraSocial,
                                      Posicion = String.IsNullOrEmpty(po.nombre) ? "Sin posición" : po.nombre,
                                      NombreApellido = e.NombreApellido,
                                      EmpleadoActivo = e.EmpleadoActivo
                                  }).FirstOrDefaultAsync();
            return empleado;
        }

        private bool EsEmpleadoRRHH()
        {
            return User.FindFirst(ClaimTypes.Role).Value == nameof(Rol.EmpleadoRRHH);
        }
    }
}
