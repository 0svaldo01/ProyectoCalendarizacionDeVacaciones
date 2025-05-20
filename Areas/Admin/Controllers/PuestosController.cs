using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProyectoCalendarizacionDeVacaciones.Models;

namespace ProyectoCalendarizacionDeVacaciones.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PuestosController : Controller
    {
        public VacacionescfeContext Context { get; }

        public PuestosController(VacacionescfeContext context)
        {
            Context = context;
        }

        public IActionResult Index()
        {
            var data = Context.Puesto.OrderBy(x => x.NombrePuesto).Where(x=>x.Estado==0);
            return View(data);
        }

        #region CREATE
        [HttpGet]
        public IActionResult Agregar()
        {
            ViewData["Departamentos"] = new SelectList(Context.Departamento, "IdDepartamento", "Nombre");
            ViewData["Puestos"] = new SelectList(Context.Puesto.Where(p => p.Estado == 0), "IdPuesto", "NombrePuesto");
            ViewData["Roles"] = new SelectList(Context.Roles, "IdRol", "NombreRol");
            ViewData["Jefes"] = new SelectList(Context.Usuario.Where(u => u.Estado == 0), "IdUsuario", "Nombre");

            return View(new Usuario());
        }
        [HttpPost]
        public IActionResult Agregar(Puesto p)
        {
            var exs = Context.Puesto.Any(x => x.NombrePuesto == p.NombrePuesto);
            if (exs)
            {
                ModelState.AddModelError("", "El puesto ya esta registrado");
            }
            if (string.IsNullOrEmpty(p.NombrePuesto))
            {
                ModelState.AddModelError("", "Debes escribir un nombre para el puesto");
            }
            if (ModelState.IsValid)
            {
                p.Estado = 0;
                Context.Add(p);
                Context.SaveChanges();
                return Redirect("~/Admin/Puestos/Index");
            }
            return View(p);
        }
        #endregion
        #region UPDATE
        [HttpGet]
        public IActionResult Editar(int id)
        {
            var ex = Context.Puesto.FirstOrDefault(x => x.IdPuesto == id);
            if (ex == null)
            {
                return Redirect("~/Admin/Puestos/Index");
            }
            return View(ex);
        }
        [HttpPost]
        public IActionResult Editar(Puesto p)
        {
           var exs = Context.Puesto.Any(x => x.NombrePuesto == p.NombrePuesto && x.IdPuesto != p.IdPuesto);
            if (exs)
            {
                ModelState.AddModelError("", "El puesto ya esta registrado");
            }
            if (string.IsNullOrEmpty(p.NombrePuesto))
            {
                ModelState.AddModelError("", "Debes escribir un nombre para el puesto");
            }
            var puesed = Context.Puesto.FirstOrDefault(x => x.IdPuesto == p.IdPuesto);
            if (ModelState.IsValid)
            {

                puesed.NombrePuesto = p.NombrePuesto;
                puesed.Estado = 0;            
                Context.SaveChanges();
                return Redirect("~/Admin/Puestos/Index");
            }
            return View(p);
        }
        #endregion

        #region DELETE (Baja lógica)
        [HttpGet]
        public IActionResult Eliminar(int id)
        {
            var puesto = Context.Puesto.FirstOrDefault(x => x.IdPuesto == id && x.Estado == 0);
            if (puesto == null)
            {
               
                return Redirect("~/Admin/Puestos/Index");
            }

            return View(puesto);
        }

        [HttpPost]
        public IActionResult Eliminar(Puesto p)
        {
            var puesto = Context.Puesto.FirstOrDefault(x => x.IdPuesto == p.IdPuesto && x.Estado == 0);
            if (puesto == null)
            {
                ModelState.AddModelError("", "El puesto ya fue eliminado o no existe.");
                return Redirect("~/Admin/Puestos/Index");
            }

            
            puesto.Estado = 1;
            Context.SaveChanges();

            return Redirect("~/Admin/Puestos/Index");
        }
        #endregion

    }
}
