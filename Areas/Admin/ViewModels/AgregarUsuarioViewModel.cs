using ProyectoCalendarizacionDeVacaciones.Models;

namespace ProyectoCalendarizacionDeVacaciones.Areas.Admin.ViewModels
{
    public class AgregarUsuarioViewModel
    {
        public Usuario Usuario { get; set; } =new Usuario();
        public IEnumerable<Roles> ListaRoles { get; set; } = new List<Roles>();
        public IEnumerable<Puesto> ListaPuestos { get; set; } = new List<Puesto>();
        public IEnumerable<Departamento> ListaDepartamentos { get; set; } = new List<Departamento>();
        public IEnumerable<Usuario> ListaJefesDepartamento { get; set; } = new List<Usuario>();
    }
}
