using System;
using System.Collections.Generic;

namespace ProyectoCalendarizacionDeVacaciones.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string? RpeRtt { get; set; }

    public string Nombre { get; set; } = null!;

    public string Contraseña { get; set; } = null!;

    public int DepartamentoId { get; set; }

    public int TipoContratoId { get; set; }

    public DateOnly FechaIngreso { get; set; }

    public int RolId { get; set; }

    public bool? EsJefeDepartamento { get; set; }

    public int? JefeDepartamentoId { get; set; }

    public virtual Departamentos Departamento { get; set; } = null!;

    public virtual ICollection<Usuario> InverseJefeDepartamento { get; set; } = new List<Usuario>();

    public virtual Usuario? JefeDepartamento { get; set; }

    public virtual Rol Rol { get; set; } = null!;

    public virtual ICollection<SolicitudVacaciones> SolicitudVacacionesJefeResponsable { get; set; } = new List<SolicitudVacaciones>();

    public virtual ICollection<SolicitudVacaciones> SolicitudVacacionesUsuario { get; set; } = new List<SolicitudVacaciones>();

    public virtual TipoContrato TipoContrato { get; set; } = null!;
}
