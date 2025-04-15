using System;
using System.Collections.Generic;

namespace ProyectoCalendarizacionDeVacaciones.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string RpRt { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string Password { get; set; } = null!;

    public DateTime FechaDeIngreso { get; set; }

    public string JefeDirecto { get; set; } = null!;

    public int Estado { get; set; }

    public int IdRol { get; set; }

    public int IdDepartamento { get; set; }

    public int IdPuesto { get; set; }

    public virtual Departamento IdDepartamentoNavigation { get; set; } = null!;

    public virtual Puesto IdPuestoNavigation { get; set; } = null!;

    public virtual Roles IdRolNavigation { get; set; } = null!;

    public virtual ICollection<Solicitudvacacion> Solicitudvacacion { get; set; } = new List<Solicitudvacacion>();

    public virtual ICollection<Vacaciones> Vacaciones { get; set; } = new List<Vacaciones>();
}
