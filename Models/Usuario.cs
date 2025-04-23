using System;
using System.Collections.Generic;

namespace ProyectoCalendarizacionDeVacaciones.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string RpeRtt { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string Password { get; set; } = null!;

    public DateOnly FechaDeIngreso { get; set; }

    public int? IdjefeDirecto { get; set; }

    public int Estado { get; set; }

    public int IdRol { get; set; }

    public int IdDepartamento { get; set; }

    public int IdPuesto { get; set; }

    public virtual Departamento IdDepartamentoNavigation { get; set; } = null!;

    public virtual Puesto IdPuestoNavigation { get; set; } = null!;

    public virtual Roles IdRolNavigation { get; set; } = null!;

    public virtual Usuario? IdjefeDirectoNavigation { get; set; }

    public virtual ICollection<Usuario> InverseIdjefeDirectoNavigation { get; set; } = new List<Usuario>();

    public virtual ICollection<Solicitudvacacion> Solicitudvacacion { get; set; } = new List<Solicitudvacacion>();

    public virtual ICollection<Vacaciones> Vacaciones { get; set; } = new List<Vacaciones>();
}
