using Microsoft.AspNetCore.Mvc;
using ProyectoCalendarizacionDeVacaciones.Models;

namespace ProyectoCalendarizacionDeVacaciones.Areas.Admin.Controllers
{
    public class DepartamentosController : Controller
    {
        public DepartamentosController(VacacionescfeContext context)
        {
            Context = context;
        }

        public VacacionescfeContext Context { get; }

        public IActionResult Index()
        {
            var data = Context.Departamento.OrderBy(x => x.NombreDepartamento).Where(x => x.Estado == 0);
            return View(data);
        }
        [HttpGet]
        public IActionResult Agregar()
        {
            var dep = new Departamento();
            return View(dep);
        }
        [HttpPost]
        public IActionResult Agregar(Departamento d)
        {
            var exs = Context.Departamento.Any(x => x.NombreDepartamento == d.NombreDepartamento);

            if (exs)
            {
                ModelState.AddModelError("", "El departamento que desea agregar ya existe");
            }
            if (string.IsNullOrEmpty(d.NombreDepartamento))
            {
                ModelState.AddModelError("", "");
            }
            if (ModelState.IsValid)
            {
                Context.Add(d);
                Context.SaveChanges();
                return Redirect("~/Admin/Departamentos/Index");
            }

            return View(d);
        }



    }
}
