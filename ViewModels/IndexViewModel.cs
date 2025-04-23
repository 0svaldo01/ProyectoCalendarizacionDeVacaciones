namespace ProyectoCalendarizacionDeVacaciones.ViewModels
{
    public class IndexViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string RPE_RTT { get; set; }

        public DateOnly FechaDeIngreso { get; set; }

        public string IdDepartamento { get; set; }
        public string IdPuesto { get; set; }
    }
}
