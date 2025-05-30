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
		
			var idClaim = User.FindFirst("IdUsuario")?.Value;

			if (int.TryParse(idClaim, out int idUsuario))
			{
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
					var antig = DateTime.Today.Year - usuario.FechaDeIngreso.Year;


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
		public IActionResult ProgramarVacaciones(Solicitudvacacion s)
		{
			var exs = Context.Solicitudvacacion.Any(x => x.IdSolicitudVacacion == s.IdSolicitudVacacion);

			#region CalcularTotalDias
			//var totaldiasUs = Context.Usuario.FirstOrDefault(x => x.IdUsuario == u.IdUsuario);

			//int diasprogrmables;

			//var antiguedad = DateTime.Today.Year - totaldiasUs.FechaDeIngreso.Year;

			//if (antiguedad == 1)
			//{
			//	diasprogrmables = 12;
			//}
			//if (antiguedad == 2)
			//{
			//	diasprogrmables = 17;
			//}
			//if (antiguedad >= 3 && antiguedad <= 9)
			//{
			//	diasprogrmables = 20;
			//}
			//if (antiguedad >= 10 && antiguedad <= 24)
			//{
			//	diasprogrmables = 24;
			//};
			//if (antiguedad >= 25)
			//{
			//	diasprogrmables = 31;
			//}
			#endregion

			if (exs)
			{
				ModelState.AddModelError("", "Ya se encuentra registrada la solicitud");
			}
			if (s.FechaInicio ==null)
			{
                ModelState.AddModelError("", "Ingresa la fecha de inicio para  la solicitud");
            }
            if (s.FechaFin == null)
            {
                ModelState.AddModelError("", "Ingresa la fecha de fin para  la solicitud");
            }

            var idClaim = User.FindFirst("IdUsuario")?.Value;

			if (int.TryParse(idClaim, out int idUs))
			{

				var usuario = Context.Usuario
					.Include(u => u.IdDepartamentoNavigation)
					.Include(u => u.IdPuestoNavigation)
					.Include(u => u.IdjefeDirectoNavigation)
					.FirstOrDefault(u => u.IdUsuario == idUs);

				if (usuario == null)
				{
					return RedirectToAction("Login", "Login");
				}

                if (ModelState.IsValid)
                {
                    s.IdUsuario = usuario.IdUsuario;
                    s.Estado = 0;
                    Context.Add(s);
                    Context.SaveChanges();
                    return Redirect("~/Home/Index");
                }
                return View(s);
			}
            return RedirectToAction("Login", "Login");
        }

		public IActionResult SolicitudesEnviadas()
		{
            var idClaim = User.FindFirst("IdUsuario")?.Value;

			if (int.TryParse(idClaim, out int idUsuario))
			{
				var us = Context.Usuario
					.Include(u => u.IdDepartamentoNavigation)
					.Include(u => u.IdPuestoNavigation)
					.Include(u => u.IdjefeDirectoNavigation)
					.FirstOrDefault(u => u.IdUsuario == idUsuario);

				var soli = Context.Solicitudvacacion.OrderBy(x => x.FechaInicio).Where(X => X.IdUsuario == us.IdUsuario);

				if (us == null || soli == null)
				{
					return RedirectToAction("Login", "Login");
				}

				if (us != null)
				{
					return View(soli);

				}

			}
			return RedirectToAction("Login", "Login");
		}

		public IActionResult ListaEmpleados()
		{
			return View();
		}

	}
}
