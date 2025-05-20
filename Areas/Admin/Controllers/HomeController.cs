using Microsoft.AspNetCore.Mvc;

namespace ProyectoCalendarizacionDeVacaciones.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
