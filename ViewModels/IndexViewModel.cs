using ProyectoCalendarizacionDeVacaciones.Models;

namespace ProyectoCalendarizacionDeVacaciones.ViewModels
{
    public class IndexViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string RPE_RTT { get; set; }

        public DateOnly FechaDeIngreso { get; set; }
        public int Antig { get; set; }

        public string NombreDepartamento { get; set; }
        public string NombrePuesto { get; set; }
        public string NombreJefe { get; set; }
    }
}
