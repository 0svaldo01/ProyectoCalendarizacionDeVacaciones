﻿using System;
using System.Collections.Generic;

namespace ProyectoCalendarizacionDeVacaciones.Models;

public partial class Solicitudvacacion
{
    public int IdSolicitudVacacion { get; set; }

    public int IdUsuario { get; set; }

    public DateOnly FechaInicio { get; set; }

    public DateOnly FechaFin { get; set; }

    public int Estado { get; set; }

    public string? Comentarios { get; set; }

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
