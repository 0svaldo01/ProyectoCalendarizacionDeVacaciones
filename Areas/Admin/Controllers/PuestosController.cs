using Microsoft.AspNetCore.Mvc;
using ProyectoCalendarizacionDeVacaciones.Models;

namespace ProyectoCalendarizacionDeVacaciones.Areas.Admin.Controllers
{
    public class PuestosController : Controller
    {
        public IActionResult Index()
        {
            return View();

        }

        #region CREATE
        [HttpGet]


        public IActionResult Agregar()
        {
            return View();
        }
        [HttpPost]
        [HttpGet]
        public IActionResult Agregar(Puesto p)
        {
            return View();
        }
        #endregion
    }
}
