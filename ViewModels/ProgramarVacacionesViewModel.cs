namespace ProyectoCalendarizacionDeVacaciones.ViewModels
{
    public class ProgramarVacacionesViewModel
    {
        public int IdSolicitudVacacion { get; set; }

        public int IdUsuario { get; set; }

        public DateOnly FechaInicio { get; set; }

        public DateOnly FechaFin { get; set; }

        public string Estado { get; set; } = null!;
        public string? Comentarios { get; set; }

        public int Antiguedad { get; set; }

    }
}
