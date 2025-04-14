using Microsoft.AspNetCore.Mvc;

namespace ProyectoCalendarizacionDeVacaciones.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
