using Microsoft.AspNetCore.Mvc;

namespace ProyectoCalendarizacionDeVacaciones.Areas.Admin.Controllers
{
    public class PuestosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
