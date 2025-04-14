using Microsoft.AspNetCore.Mvc;

namespace ProyectoCalendarizacionDeVacaciones.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
