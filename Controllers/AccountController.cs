using Microsoft.AspNetCore.Authentication.Cookies;
using ProyectoCalendarizacionDeVacaciones.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace ProyectoCalendarizacionDeVacaciones.Controllers
{
    public class AccountController : Controller
    {
        public AccountController(ProyectoCalendarizacionDeVacaciones.Models.VacacionescfeContext context)
        {
            Context = context;
        }

        public VacacionescfeContext Context { get; }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(string rprt, string password)
        {
            var usuario = Context.Usuario.Include(x=>x.IdRolNavigation)
                .FirstOrDefault(x => x.RpRt == rprt && x.Password == password);

            if (usuario.Estado == 1)
            {
                ModelState.AddModelError("", "El usuario no se encuentra");
                return View();
            }
            if (usuario != null)
            {
                List<Claim> claims = new List<Claim>();

                claims.Add(new Claim(ClaimTypes.Name, usuario.RpRt));
                claims.Add(new Claim(ClaimTypes.Role, usuario.IdRolNavigation.NombreRol));
                claims.Add(new Claim("IdUsuario", usuario.IdUsuario.ToString()));

                //identidad
                var identidad = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(new ClaimsPrincipal(identidad));

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "El usuario o contraseña son incorrectos");
                return View();
            }
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }



    }
}
