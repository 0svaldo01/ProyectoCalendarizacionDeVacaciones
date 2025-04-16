using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoCalendarizacionDeVacaciones;
using ProyectoCalendarizacionDeVacaciones.Models;

namespace ProyectoCalendarizacionDeVacaciones.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly VacacionescfeContext context;

        public UsuarioController(VacacionescfeContext context)
        {
            this.context = context;
        }

        [Route("Empleado/{Id}")]
        public IActionResult Index(int Id)
        {
          var data = context.Usuario.OrderBy(x => x.IdUsuario == Id).Include(x => x.IdDepartamentoNavigation).Include(x => x.IdRolNavigation).Include(x => x.IdPuestoNavigation);
            return View(data);
        }

    }
}
