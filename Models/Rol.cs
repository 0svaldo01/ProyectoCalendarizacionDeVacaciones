using System;
using System.Collections.Generic;

namespace ProyectoCalendarizacionDeVacaciones.Models;

public partial class Rol
{
    public int Id { get; set; }

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<Usuario> Usuario { get; set; } = new List<Usuario>();
}
