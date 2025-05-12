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

        
        #region READ
        public IActionResult Index()
        {
            var data = Context.Usuario.OrderBy(x => x.IdUsuario).Include(x => x.IdDepartamentoNavigation).Include(x => x.IdPuestoNavigation)
                .Select(e=> new IndexUsuariosViewModel 
                {
                    RPE_RTT =e.RpeRtt,
                    Nombre =e.Nombre,
                    FechaDeIngreso =e.FechaDeIngreso,
                    IdDepartamento =e.IdDepartamentoNavigation.NombreDepartamento,
                    IdPuesto =e.IdPuestoNavigation.NombrePuesto
                });               
            return View(data);
        }
        #endregion
        #endregion
    }
}
