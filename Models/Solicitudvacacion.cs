using System;
using System.Collections.Generic;

namespace ProyectoCalendarizacionDeVacaciones.Models;

public partial class Solicitudvacacion
{
    public int IdSolicitudVacacion { get; set; }

    public int IdUsuario { get; set; }

    public DateOnly FechaInicioFin { get; set; }

    public string Estado { get; set; } = null!;

    public string? Comentarios { get; set; }

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
