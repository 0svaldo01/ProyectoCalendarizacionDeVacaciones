using System;
using System.Collections.Generic;

namespace ProyectoCalendarizacionDeVacaciones.Models;

public partial class SolicitudVacaciones
{
    public int Id { get; set; }

    public int UsuarioId { get; set; }

    public DateOnly FechaInicio { get; set; }

    public DateOnly FechaFin { get; set; }

    public DateOnly FechaSolicitud { get; set; }

    public int EstadoId { get; set; }

    public string? MotivoRechazo { get; set; }

    public DateOnly? FechaRespuesta { get; set; }

    public int? JefeResponsableId { get; set; }

    public virtual EstadoSolicitud Estado { get; set; } = null!;

    public virtual Usuario? JefeResponsable { get; set; }

    public virtual Usuario Usuario { get; set; } = null!;
}
