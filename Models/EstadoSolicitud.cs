using System;
using System.Collections.Generic;

namespace ProyectoCalendarizacionDeVacaciones.Models;

public partial class EstadoSolicitud
{
    public int Id { get; set; }

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<SolicitudVacaciones> SolicitudVacaciones { get; set; } = new List<SolicitudVacaciones>();
}
