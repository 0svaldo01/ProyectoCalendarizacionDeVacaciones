using ProyectoCalendarizacionDeVacaciones.Models;
using Microsoft.AspNetCore.Mvc;
using ProyectoCalendarizacionDeVacaciones.Areas.Admin.ViewModels;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.EntityFrameworkCore;

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
            var data = Context.Usuario
                .OrderBy(u => u.Nombre)
                .Include(u => u.IdDepartamentoNavigation)
                .Include(u => u.IdPuestoNavigation)
                .Include(u => u.IdRolNavigation).Where(u => u.Estado == 0).ToList();



            return View(data);
        }

        #endregion
    }
}
