using ProyectoCalendarizacionDeVacaciones.ViewModels;
using ProyectoCalendarizacionDeVacaciones.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ProyectoCalendarizacionDeVacaciones.Controllers
{
	[Authorize]
	public class HomeController : Controller
	{
		public HomeController(VacacionescfeContext context)
		{
			Context = context;
		}

		public VacacionescfeContext Context { get; }
		public IActionResult Index()
		{
			// Obtener el Id del usuario logueado desde las claims
			var idClaim = User.FindFirst("IdUsuario")?.Value;

			if (int.TryParse(idClaim, out int idUsuario))
			{
				// Cargar el usuario desde la base de datos con las relaciones
				var usuario = Context.Usuario
					.Include(u => u.IdDepartamentoNavigation)
					.Include(u => u.IdPuestoNavigation)
					.Include(u => u.IdjefeDirectoNavigation)
					.FirstOrDefault(u => u.IdUsuario == idUsuario);

				if (usuario == null)
				{
					return RedirectToAction("Login", "Login");
				}

				if (usuario != null)
				{
					// Calcular la antigüedad
					var antig = DateTime.Today.Year - usuario.FechaDeIngreso.Year;


					// Llenar el ViewModel
					var vm = new IndexViewModel
					{
						Id = usuario.IdUsuario,
						Nombre = usuario.Nombre,
						RPE_RTT = usuario.RpRt,
						FechaDeIngreso = usuario.FechaDeIngreso,
						Antig = antig,
						NombreDepartamento = usuario.IdDepartamentoNavigation?.NombreDepartamento ?? "No disponible",
						NombrePuesto = usuario.IdPuestoNavigation?.NombrePuesto ?? "No disponible",
						NombreJefe = usuario.IdjefeDirectoNavigation?.Nombre ?? "No disponible"
					};


					return View(vm);

				}
			}

			return RedirectToAction("Login", "Login");
		}

		public IActionResult PeriodosDisp()
		{
			var idClaim = User.FindFirst("IdUsuario")?.Value;

			if (int.TryParse(idClaim, out int idUsuario))
			{

				var usuario = Context.Vacaciones.OrderBy(x => x.Vigencia).Include(x => x.IdUsuarioNavigation).Where(u => u.IdUsuario == idUsuario);
				return View(usuario);
			}
			return RedirectToAction("Login", "Login");
		}
		public IActionResult ProgramarVacaciones()
		{
			var vac = new Solicitudvacacion();
			return View(vac);
		}
		[HttpPost]
		public IActionResult ProgramarVacaciones(Solicitudvacacion solicitud)
		{
			return View();
		}
    }
}
