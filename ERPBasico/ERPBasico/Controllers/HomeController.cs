using ERPBasico.Data;
using ERPBasico.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;


namespace ERPBasico.Controllers
{
  
    public class HomeController : Controller
    {
        private readonly ModelContext _context;
        private readonly ILogger<HomeController> _logger;
        private ISeguridad _seguridad = new SeguridadBasica();

        public HomeController(ILogger<HomeController> logger,ModelContext context)
        {
            _logger = logger;
            _context = context;           
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Ingresar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Ingresar(string usuario,string password)
        {
            //var empleado = new Empleado
            //{
            //    Nombre = "Franco",
            //    Apellido = "Redoni",
            //    Direccion = "Domicilio Test",
            //    Dni = 11111111,
            //    Email = usuario,
            //    EmpleadoActivo = true,
            //    FechaAlta = DateTime.Now,
            //    Legajo = 6548,
            //    Password = _seguridad.EncriptarPass(password),
            //    Rol = Rol.EmpleadoRRHH
            //};
            //_context.Empleados.Add(empleado);
            //_context.SaveChanges();

            var urlIngreso = TempData["UrlIngreso"] as string;
            if (!string.IsNullOrEmpty(usuario) && !string.IsNullOrEmpty(password))
            {

                // Verificamos que exista el usuario
                var user = await _context.Empleados.FirstOrDefaultAsync( user => user.Email == usuario);
                if (user != null)
                {
                    // Verificamos que coincida la contraseña
                    var contraseña = _seguridad.EncriptarPass(password);
                    if (contraseña.SequenceEqual(user.Password))
                    {
                        // Creamos los Claims (credencial de acceso con informacion del usuario)
                        ClaimsIdentity identidad = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

                        // Agregamos a la credencial el nombre de usuario
                        identidad.AddClaim(new Claim(ClaimTypes.Name, usuario));

                        // Agregamos a la credencial el nombre del estudiante/administrador
                        identidad.AddClaim(new Claim(ClaimTypes.GivenName, user.Nombre));

                        // Agregamos a la credencial el Rol
                        identidad.AddClaim(new Claim(ClaimTypes.Role, user.Rol.ToString()));

                        // Agregamos el ID de Empleado
                        identidad.AddClaim(new Claim("EmpleadoId", user.Id.ToString()));

                        ClaimsPrincipal principal = new ClaimsPrincipal(identidad);
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                        if (!string.IsNullOrEmpty(urlIngreso))
                        {
                            return Redirect(urlIngreso);
                        }
                        else
                        {
                            // Redirigimos a la pagina principal
                            return RedirectToAction("Index", "Empleado");
                        }
                    }
                }
            }
            ViewBag.ErrorEnLogin = "Verifique el usuario y contraseña";
            TempData["UrlIngreso"] = urlIngreso; // Volvemos a enviarla en el TempData para no perderla

            return View();
        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Salir()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public IActionResult AccesoDenegado()
        {
            return View();
        }




    }
}
