using ProyectoCalendarizacionDeVacaciones.ViewModels;
using ProyectoCalendarizacionDeVacaciones.Models;
using Microsoft.AspNetCore.Mvc;

namespace ProyectoCalendarizacionDeVacaciones.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(VacacionescfeContext context)
        {
            Context = context;
        }

        public VacacionescfeContext Context { get; }

        public IActionResult Index()
        {
            
            return View();
        }
    }
}
