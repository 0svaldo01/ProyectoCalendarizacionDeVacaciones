using ProyectoCalendarizacionDeVacaciones.Models;
using Microsoft.AspNetCore.Mvc;
using ProyectoCalendarizacionDeVacaciones.Areas.Admin.ViewModels;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProyectoCalendarizacionDeVacaciones.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsuariosController : Controller
    {
        public UsuariosController(VacacionescfeContext context)
        {

            Context = context;
        }
        public VacacionescfeContext Context { get; }


        #region C.R.U.D

        public IActionResult Index()
        {
            var data = Context.Usuario.OrderBy(x => x.Nombre).Include(x => x.IdDepartamentoNavigation)
                .Include(x => x.IdPuestoNavigation).Include(x => x.IdRolNavigation).Where(x=>x.Estado ==0);
            return View(data);
        }

        #region CREATE
        [HttpGet]
        public IActionResult Agregar()
        {
            AgregarUsuarioViewModel vm = new();

            vm.ListaDepartamentos = Context.Departamento.OrderBy(x => x.NombreDepartamento).Where(x=>x.Estado ==0);
            vm.ListaPuestos = Context.Puesto.OrderBy(x => x.NombrePuesto).Where(x => x.Estado == 0);
            vm.ListaRoles = Context.Roles.OrderBy(x => x.NombreRol).Where(x => x.Estado == 0);
            vm.ListaJefesDepartamento = Context.Usuario.Where(x => x.IdRol == 2).Where(x => x.Estado == 0);

            return View(vm);
        }

        [HttpPost]
        public IActionResult Agregar(AgregarUsuarioViewModel u)
        {
            var existe = Context.Usuario.Any(x => x.RpRt == u.Usuario.RpRt);

            if (existe)
            {
                ModelState.AddModelError("", "Este RP/RT ya está registrado.");
            }
            if (string.IsNullOrEmpty(u.Usuario.Nombre))
            {
                ModelState.AddModelError("", "Debes ingresar un nombre.");
            }
            if (string.IsNullOrEmpty(u.Usuario.Password))
            {
                ModelState.AddModelError("", "Debes ingresar una contraseña");
            }
            if (u.Usuario.FechaDeIngreso > DateOnly.FromDateTime(DateTime.Today)) 
            {
                ModelState.AddModelError("", "La fecha de ingreso no puede ser mayor a la fecha actual");
            }
            if(u.ListaPuestos == null)
            {
                ModelState.AddModelError("", "Debes ingresar una puesto");
            }

            if (ModelState.IsValid)
            {
               u.Usuario.Estado = 0;
               Context.Add(u.Usuario);                
                Context.SaveChanges();
                return Redirect("~/Admin/Usuarios/Index");
            }
            u.ListaDepartamentos = Context.Departamento.OrderBy(x => x.NombreDepartamento).Where(x => x.Estado == 0);
            u.ListaPuestos = Context.Puesto.OrderBy(x => x.NombrePuesto).Where(x => x.Estado == 0);
            u.ListaRoles = Context.Roles.OrderBy(x => x.NombreRol).Where(x => x.Estado == 0);
            u.ListaJefesDepartamento = Context.Usuario.Where(x => x.IdRol == 2).Where(x => x.Estado == 0);

            return View(u);
        }
        #endregion


        #endregion
    }
}
